using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursue : SteeringBehaviour
{
    public Boid leader;
    Vector3 targetPos;
    Vector3 worldTarg;
    public Vector3 offset;
    public Transform leaderTrans;

    void Start()
    {
        offset = transform.position - leader.transform.position; //Calculates the distance between the pursuing boid and the leader boid
        offset = Quaternion.Inverse(leader.transform.rotation) * offset;
    }
    public override Vector3 Calculate()
    {
        worldTarg = leaderTrans.TransformPoint(offset); //Transforms the offset from local space to world space
        float dist = Vector3.Distance(worldTarg, transform.position);
        float time = dist / boid.maxSpeed;
        Vector3 posMult = leader.vel * time;
        targetPos = worldTarg + posMult;
        force = boid.SeekingForce(targetPos);
        return force;
    }
    private void Update()
    {
        if (leader != null)
        {
            leaderTrans = leader.transform;
        }
    }
}
