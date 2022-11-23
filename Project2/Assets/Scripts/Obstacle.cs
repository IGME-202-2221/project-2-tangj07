using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float radius = 6f;
    public float width, height;
    public Vector3 Position
    {
        get { return transform.position; }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
    }
    void Start()
    {
        width = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }
}

