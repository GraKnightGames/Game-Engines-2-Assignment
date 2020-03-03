using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursue : SteeringBehaviour
{
    public Boid leader;
Vector3 targetPos;
    Vector3 worldTarg;
    Vector3 offset;
    private float maxSpeed = 5;

    void Start()
    {
        offset = transform.position - leader.transform.position;
        offset = Quaternion.Inverse(leader.transform.rotation) * offset;
    }
    public override Vector3 Calculate()
    {
        worldTarg = leader.transform.TransformPoint(offset);
        float dist = Vector3.Distance(worldTarg, transform.position);
        float time = dist / maxSpeed;
        targetPos = worldTarg + (leader.vel * time);
        return leader.ArrivingForce(targetPos);
    }
}
