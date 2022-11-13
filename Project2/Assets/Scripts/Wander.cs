using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Agent
{
    [SerializeField]
    Vector2 worldsize;
    Vector3 boundforce;
    [SerializeField]
    float boundstimecheck = 2;
    private void Awake()
    {
        worldsize.y = Camera.main.orthographicSize;
        worldsize.x = Camera.main.aspect * worldsize.y;
        worldsize = worldsize * 0.7f;
    }
    protected override void CalcSteeringForces()
    {
        totalSteeringForce += Wander();
        boundforce = StayInBounds(worldsize, boundstimecheck);
        totalSteeringForce += boundforce;
        totalSteeringForce += Seperation();
    }
}
