using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Agent
{
    public Fleer assignedprey;
    public Seeker[] other;
    public bool wander = true;
    [SerializeField]
    Vector2 worldsize;
    private void Awake()
    {
        Camera camera = Camera.main;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        worldsize.y = cameraHeight * 0.5f;
        worldsize.x = cameraHeight * screenAspect / 2;
        worldsize = worldsize * 0.5f;
    }
    protected override void CalcSteeringForces()
    {
        if (wander)
        {
            totalSteeringForce += Wander();
            totalSteeringForce += Seperation();
            totalSteeringForce += AvoidObstacle();
        }
        else
        {
            if (assignedprey != null)
            {
                totalSteeringForce += Seperation();
                totalSteeringForce += Seek(assignedprey.transform.position);
                totalSteeringForce += AvoidObstacle();
            }
        }
    }
    public override void Update()
    {
        base.Update();
        other = FindObjectsOfType<Seeker>();
    }
    public Vector3 Seperation()
    {
        Vector3 seperateForce = Vector3.zero;
        float sqrDis;
        foreach (Seeker temp in other)
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
