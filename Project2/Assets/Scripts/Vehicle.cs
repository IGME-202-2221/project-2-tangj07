using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    public Obstacle[] obstacles;
    [SerializeField]
    float speed = 1f;
    Vector3 position = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    private float delay = 0.3f, bullettime=-1;
    public GameObject bullet;
    bool fire;
    [SerializeField]
    CollisionManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<CollisionManager>();
        obstacles = FindObjectsOfType<Obstacle>();
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        direction.Normalize();
        velocity = direction * speed * Time.deltaTime;
        position += velocity;
        if (manager.dead)
        {
            direction = Vector3.zero;
        }
        if (direction != Vector3.zero)
        {
            transform.position = position;
        }
        //warping stuff
        Camera camera = Camera.main;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        float height = cameraHeight*0.5f;
        float width = cameraHeight * screenAspect/2;
        if (position.y > height)
        {
            position.y = -1*height;
        }
        else if (position.y < -1 * height)
        {
            position.y = height;
        }
        else if (position.x > width)
        {
            position.x = -1 * width;
        }
        else if (position.x < -1 * width)
        {
            position.x = width;
        }
        if (fire && bullettime>0)
        {
            bullettime -= Time.deltaTime;
        }
        else if (bullettime <= 0)
        {
            fire = false;
        }
        //ObstacleStuff();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }
    public void Pewpew()
    {
        if (!manager.dead)
        {
            if (bullettime <= 0)
            {
                Instantiate(bullet, new Vector3(position.x, position.y, 0), transform.rotation);
                bullettime = delay;
            }
            fire = true;
        }
    }
    public void ObstacleStuff()
    {
        if (position.x < (-obstacles[0].Position.x + (obstacles[0].width / 2))&& position.x < (-obstacles[0].Position.x - (obstacles[0].width / 2)))
        {
            if(position.y< (obstacles[0].Position.y + (obstacles[0].height / 2))&& position.y > (obstacles[0].Position.y - (obstacles[0].height / 2)))
            {
                if(position.x < (-obstacles[0].Position.x + (obstacles[0].width / 2))) { position.x = -obstacles[0].Position.x + (obstacles[0].width / 2); }
            }
        }
    }
}
