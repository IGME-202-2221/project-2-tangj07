using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public float hp;
    bool dead = false;
    [HideInInspector]
    public float x, y, width, height;
    [HideInInspector]
    public CollisionDetection temp;

    // Start is called before the first frame update
    void Start()
    {
        hp = 1f;
        StartCoroutine(deathdelay());
    }

    // Update is called once per frame
    void Update()
    {
        temp = gameObject.GetComponent(typeof(CollisionDetection)) as CollisionDetection;
        x = temp.x;
        y = temp.y;
        width = temp.width;
        height = temp.height;
    }
    bool death()
    {
        if (hp <= 0) { dead = true; }
        return dead;
    }
    IEnumerator deathdelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (death())
            {
                Destroy(gameObject);
            }
        }
    }
}
