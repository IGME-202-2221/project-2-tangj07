using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject wolf;
    [SerializeField]
    GameObject follower;
    [SerializeField]
    GameObject power;
    [SerializeField]
    CollisionManager manager;

    float two = 2;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<CollisionManager>();
        StartCoroutine(TimeSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.spawn)
        {
            GameObject temp;
            temp = Instantiate(follower, new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4.5f, 4.5f), 0), transform.rotation);
        }
    }
    IEnumerator TimeSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(two);
            if (!manager.dead) { Spawn(); Spawn2(); }
        }
    }
    void Spawn()
    {
        GameObject temp;
        temp = Instantiate(wolf, new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4.5f, 4.5f), 0), transform.rotation);
    }
    void Spawn2()
    {
        GameObject temp;
        temp = Instantiate(power, new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4.5f, 4.5f), 0), transform.rotation);
    }
}
