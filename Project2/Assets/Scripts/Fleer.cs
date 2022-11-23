using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleer : Agent
{
    public Seeker predator;
    public GameObject player;
    public bool dead;
    public Fleer[] other;
    public bool follow = true;
    protected override void CalcSteeringForces()
    {
        player = GameObject.Find("MasterSheep");
        if (follow)
        {
            totalSteeringForce += Seperation();
            totalSteeringForce += Seek(player.transform.position);
            totalSteeringForce += AvoidObstacle();
        }
        else
        {
            if (predator != null)
            {
                totalSteeringForce += Seperation();
                totalSteeringForce += Flee(predator.transform.position);
                totalSteeringForce += AvoidObstacle();
            }
        }
    }
    public override void Update()
    {
        base.Update();
        other = FindObjectsOfType<Fleer>();
        if (dead) { Destroy(gameObject); }
    }
    public Vector3 Seperation()
    {
        Vector3 seperateForce = Vector3.zero;
        float sqrDis;
        foreach (Fleer temp in other)
        {
            sqrDis = Vector3.SqrMagnitude(physicsObject.Position - temp.physicsObject.Position);
            if (sqrDis != 0)
            {
                seperateForce += Flee(temp.physicsObject.Position) * (0.3f / sqrDis);
            }
        }
        return seperateForce;
    }
}
