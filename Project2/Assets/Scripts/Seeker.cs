using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Agent
{
    [SerializeField]
    GameObject prey;
    protected override void CalcSteeringForces()
    {
        totalSteeringForce += Seek(prey.transform.position);
    }
}
