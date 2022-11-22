using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleer : Agent
{
    [SerializeField]
    GameObject predator;
    public bool dead;
    protected override void CalcSteeringForces()
    {
        totalSteeringForce += Flee(predator.transform.position);
    }
    public override void Update()
    {
        base.Update(); 
        if (dead)
        {
            Destroy(gameObject);
        }
    }
}
