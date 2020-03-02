using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    List<SteeringBehaviour> strBehaviours = new List<SteeringBehaviour>();
    public Vector3 vel = Vector3.zero;
    public Vector3 accel = Vector3.zero;
    public Vector3 force = Vector3.zero;

    public float mass = 1.0f;

    public static float maxSpeed = 5;
    public float maxForce = 10;
    public int i;
    public float dist;
    [Range(0.0f, 10.0f)]
    public float damping = 0.1f;

    [Range(0.1f, 1.0f)] [SerializeField] private float banking;


    // Start is called before the first frame update
    void Start()
    {
        SteeringBehaviour[] strBehaviours = GetComponents<SteeringBehaviour>();
        foreach (SteeringBehaviour b in strBehaviours)
        {
            this.strBehaviours.Add(b);
        }
    }

    public Vector3 SeekingForce(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired = desired * maxSpeed;
        return desired - vel;
    }

    public Vector3 ArrivingForce(Vector3 target, float slowingDistance = 20.0f)
    {
        Vector3 toTarget = target - transform.position;

        float distance = toTarget.magnitude;
        if (distance < 0.1f)
        {
            return Vector3.zero;
        }
        float rampMult = distance / slowingDistance;
        float ramped = maxSpeed * rampMult;

        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = clamped * (toTarget / distance);

        return desired - vel;
    }

    public Vector3 Calculate()
    {
        force = Vector3.zero;

        foreach (SteeringBehaviour b in strBehaviours)
        {
            if (b.isActiveAndEnabled)
            {
                force += b.Calculate() * b.boidWeight;

                float f = force.magnitude;
                if (f >= maxForce)
                {
                    force = Vector3.ClampMagnitude(force, maxForce);
                    break;
                }
            }
        }

        return force;
    }

    void Update()
    {
        force = Calculate();
        Vector3 newAccel = force / mass;
        accel = Vector3.Lerp(accel, newAccel, Time.deltaTime);
        vel += accel * Time.deltaTime;

        vel = Vector3.ClampMagnitude(vel, maxSpeed);

        if (vel.magnitude > 0)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (accel * banking), Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + vel, tempUp);

            transform.position += vel * Time.deltaTime;
            vel *= (1.0f - (damping * Time.deltaTime));
        }
    }
}
