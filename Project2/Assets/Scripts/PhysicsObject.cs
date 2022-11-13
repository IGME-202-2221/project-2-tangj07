using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    Vector3 acceleration = Vector3.zero;
    Vector3 position = Vector3.zero;
    public Vector3 Direction
    {
        get { return direction; }
    }
    public Vector3 Velocity
    {
        get { return velocity; }
    }
    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    [SerializeField]
    float mass = 1f;
    [SerializeField]
    bool useGravity = true;
    [SerializeField]
    Vector3 gravity = Vector3.down;
    [SerializeField]
    bool useFriction = false;
    [SerializeField]
    float coefficient = 1f;

    float width, height;
    void Start()
    {
        position = transform.position;
        height = Camera.main.orthographicSize;
        width = Camera.main.orthographicSize * Camera.main.aspect;
        velocity = Vector3.ClampMagnitude(velocity, 150);

        direction = Random.insideUnitCircle.normalized;
    }
    // Update is called once per frame
    void Update()
    {
        if (useGravity) { ApplyGravity(); }
        if (useFriction) { ApplyFriction(); }

        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        if (velocity != Vector3.zero)
        {
            direction = velocity.normalized;
        }
        transform.position = position;
        acceleration = Vector3.zero;

        Bounce();
    }
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }
    void ApplyGravity()
    {
        ApplyForce(gravity * mass);
    }
    void ApplyFriction()
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coefficient;
        ApplyForce(friction);
    }
    void Bounce()
    {
        if (position.x > width)
        {
            position.x = width;
            velocity.x *= -1;
        }
        if (position.x < -1 * width)
        {
            velocity.x *= -1;
            position.x = -width;
        }
        if (position.y > height)
        {
            velocity.y *= -1;
            position.y = height;
        }
        if (position.y < -1 * height)
        {
            velocity.y *= -1;
            position.y = -height;
        }
    }
}
