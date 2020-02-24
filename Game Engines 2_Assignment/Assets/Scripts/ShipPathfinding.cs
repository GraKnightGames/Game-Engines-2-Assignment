using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPathfinding : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 force = Vector3.zero;

    public float mass = 1.0f;

    public float maxSpeed = 5;
    public float maxForce = 10;
    public int i = 0;

    public float speed = 0;
    public float dist;

    public Vector3 target;
    public Transform[] targetTransforms;

    private bool arrived = false;
    public float changingDistance = 4.0f;
    [Range(0.0f, 0.2f)]
    public float banking = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < targetTransforms.Length - 1; i++)
        {
            Gizmos.DrawLine(targetTransforms[i].position, targetTransforms[i + 1].position);
        }
        for (int i = 0; i < targetTransforms.Length; i++)
        {
            Gizmos.DrawCube(targetTransforms[i].position, new Vector3(1, 1, 1));
            Gizmos.DrawLine(targetTransforms[i].position, targetTransforms[0].position);
        }
    }


    Vector3 Seek(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        Vector3 desired = toTarget.normalized * maxSpeed;

        return desired - velocity;
    }

    public Vector3 CalculateForce()
    {
        Vector3 toTarget = target - transform.position;
        dist = toTarget.magnitude;
        Vector3 force = Vector3.zero;
        force += Seek(target);
        return force;
    }

    public void Switching()
    {
        Vector3 toTarget = target - transform.position;
        dist = toTarget.magnitude;
        if (dist <= changingDistance && !arrived)
        {
            arrived = true;
        }
        else
        {
            arrived = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            target = targetTransforms[i].position;
        }
        force = CalculateForce();
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        speed = velocity.magnitude;
        if (speed > 0)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + velocity, tempUp);
        }
        Switching();
        if (arrived)
        {
            i += 1;
            arrived = false;
        }
        else if (!arrived)
        {

        }
        if (i > (targetTransforms.Length - 1))
        {
            i = 0;
        }
    }
}
