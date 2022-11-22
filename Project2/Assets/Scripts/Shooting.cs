using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed = 5f, time = 0.1f;

    [HideInInspector]
    public float x, y, width, height;
    [HideInInspector]
    public CollisionDetection temp;

    // Update is called once per frame
    void Update()
    {
		Vector3 position = transform.position, velocity = new Vector3(0, speed * Time.deltaTime, 0);
		position += transform.rotation * velocity;
		transform.position = position;time -= Time.deltaTime;
        if (time <= 0){ Destroy(gameObject); }
        temp = gameObject.GetComponent(typeof(CollisionDetection)) as CollisionDetection;
        x = temp.x;
        y = temp.y;
        width = temp.width;
        height = temp.height;
	}
}
