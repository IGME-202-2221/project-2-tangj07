using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    [SerializeField]
    protected PhysicsObject physicsObject;
    AgentManager manager;
    [SerializeField]
    float maxSpeed = 2f, maxForce = 2f;
    protected Vector3 totalSteeringForce;
    void Start()
    {
        physicsObject = GetComponent<PhysicsObject>();
        
    }
    public void Init(AgentManager manager)
    {
        this.manager = manager;
    }
    public virtual void Update()
    {
        totalSteeringForce = Vector3.zero;
        CalcSteeringForces();
        totalSteeringForce = Vector3.ClampMagnitude(totalSteeringForce, maxForce);
        physicsObject.ApplyForce(totalSteeringForce);
        physicsObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, physicsObject.Velocity.normalized);
    }
    protected abstract void CalcSteeringForces();
    public Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = targetPosition - transform.position;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;
        Vector3 seekForce = desiredVelocity - physicsObject.Velocity;
        return seekForce;
    }
    public Vector3 Flee(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = transform.position - targetPosition;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;
        Vector3 fleeForce = desiredVelocity - physicsObject.Velocity;
        return fleeForce;
    }
    protected Vector3 Wander()
    {
        float wanderAngle=0;
        wanderAngle+= Random.Range(-10f*Time.deltaTime, 10*Time.deltaTime);
        wanderAngle = Mathf.Clamp(wanderAngle, -45f, 45);
        Vector3 wanderPos = Quaternion.Euler(0,0,wanderAngle)*physicsObject.Direction.normalized+physicsObject.Position;
        return Seek(wanderPos);
    }
    public Vector3 StayInBounds(Vector2 bounds, float time)
    {
        Vector3 position = CalcFuturePosition(time);
        if(position.x>= bounds.x || position.x <= -bounds.x || position.y <= -bounds.y || position.y >= bounds.y)
        {
            return Seek(Vector3.zero);
        }
        else { return Vector3.zero; }
    }
    public Vector3 Seperation()
    {
        Vector3 seperateForce = Vector3.zero;
        float sqrDis;
        foreach(Agent other in manager.agents)
        {
            sqrDis = Vector3.SqrMagnitude(physicsObject.Position - other.physicsObject.Position);
            if (sqrDis != 0)
            {
                seperateForce += Flee(other.physicsObject.Position) * (1f / sqrDis);
            }
        }
        return seperateForce;
    }
    public Vector3 CalcFuturePosition(float time)
    {
        return physicsObject.Position+physicsObject.Velocity * time;
    }
}
