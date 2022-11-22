using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float radius = 1f;
    public Vector3 Position
    {
        get { return transform.position; }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
    }
}
