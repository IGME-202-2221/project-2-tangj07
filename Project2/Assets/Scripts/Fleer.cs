using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleer : Agent
{
    [SerializeField]
    GameObject predator;
    protected override void CalcSteeringForces()
    {
        totalSteeringForce += Flee(predator.transform.position);
    }
    public override void Update()
    {
        base.Update();
        if (CircleCollision(predator))
        {
            physicsObject.Position = new Vector3(Random.Range(-7.6f, 7.6f), Random.Range(-4.68f, 4.68f), 0);
        }
    }
    bool CircleCollision(GameObject seeker)
    {
        SpriteRenderer thissprite = GetComponent<SpriteRenderer>();
        SpriteRenderer seekersprite = seeker.GetComponent<SpriteRenderer>();

        float distance = Mathf.Pow(thissprite.bounds.center.x - seekersprite.bounds.center.x, 2)
            + Mathf.Pow(thissprite.bounds.center.y - seekersprite.bounds.center.y, 2);
        if (distance <= ((thissprite.bounds.max.x - thissprite.bounds.center.x) +
            (seekersprite.bounds.max.x - seekersprite.bounds.center.x)) *
            ((thissprite.bounds.max.x - thissprite.bounds.center.x) +
            (seekersprite.bounds.max.x - seekersprite.bounds.center.x)))
        { return true; }
        else { return false; }
    }
}
