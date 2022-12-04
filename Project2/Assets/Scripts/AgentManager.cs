using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentManager : MonoBehaviour
{
    public float timer;
    [SerializeField]
    Text time;
    [SerializeField]
    Text time1;
    [SerializeField]
    Text time2;
    public CollisionDetection player;
    public bool bullethit = false, dead = false, ate = false, pick = false, spawn = false, wander;
    public Shooting[] bullets;
    public Wolf[] wolves;
    public Seeker[] wolves2;
    public Fleer[] followers;
    public PowerUp[] powerUp;
    public Obstacle[] obstacles;
    [SerializeField]
    Text gameover1;
    [SerializeField]
    Text gameover2;
    [SerializeField]
    Text instructions;
    bool collide, seek;
    [SerializeField]
    GameObject wolf;
    [SerializeField]
    GameObject follower;
    [SerializeField]
    GameObject power;
    // Start is called before the first frame update
    void Start()
    {
        for (int a = 0; a < 5; a++)
        {
            GameObject temp;
            temp = Instantiate(follower, new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4.5f, 4.5f), 0), transform.rotation);
        }
        StartCoroutine(TimeSpawn());
        StartCoroutine(TimeSpawn2());
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            GameObject temp;
            temp = Instantiate(follower, new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4.5f, 4.5f), 0), transform.rotation);
        }
        spawn = false;
        wolves2 = FindObjectsOfType<Seeker>();
        bullets = FindObjectsOfType<Shooting>();
        wolves = FindObjectsOfType<Wolf>();
        followers = FindObjectsOfType<Fleer>();
        powerUp = FindObjectsOfType<PowerUp>();
        obstacles = FindObjectsOfType<Obstacle>();
        if (followers.Length <= 0) { dead = true; Gameover(); }
        if (!dead)
        {
            timer += Time.deltaTime;
            time.text = "" + timer;
            foreach (Wolf temp in wolves)
            {
                if (CircleCollision4(player, temp)) { dead = true; Gameover2(); }
                foreach (Shooting temp2 in bullets)
                {
                    bullethit = CircleCollision(temp, temp2);
                    hit(temp);
                    if (bullethit) { temp.hp -= 1; break; }
                }
            }
            for (int a = 0; a < followers.Length; a++)
            {
                for (int b = 0; b < wolves2.Length; b++)
                {
                    seek = Detection(followers[a], wolves2[b]);
                    ColorSprite2(wolves2[b]);
                    ColorSprite3(followers[a]);
                    if (Detection(followers[a], wolves2[b]))
                    {
                        wolves2[b].wander = false;
                        followers[a].follow = false;
                        wolves2[b].assignedprey = followers[a];
                        followers[a].predator = wolves2[b];
                    }
                    else
                    {
                        wolves2[b].wander = true;
                        followers[a].follow = true;
                    }
                }
            }
            for (int a = 0; a < followers.Length; a++)
            {
                for (int b = 0; b < wolves.Length; b++)
                {
                    if (CircleCollision2(followers[a], wolves[b])) { followers[a].dead = true; }
                }
            }
            foreach (PowerUp temp in powerUp)
            {
                temp.pick = CircleCollision3(player, temp);
                if (temp.pick) { spawn = true; }
            }
            foreach (Obstacle temp in obstacles)
            {
                collide = CircleCollision5(player, temp);
                ColorSprite(player);
                if (CircleCollision5(player, temp)) { break; }
            }
        }
    }
    IEnumerator TimeSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            if (!dead) { Spawn(); }
        }
    }
    IEnumerator TimeSpawn2()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            if (!dead) { Spawn2(); }
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
    private bool CircleCollision(Wolf objecta, Shooting objectb)
    {
        SpriteRenderer spritea = objecta.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteb = objectb.GetComponent<SpriteRenderer>();

        float distance = Mathf.Pow(spritea.bounds.center.x - spriteb.bounds.center.x, 2)
            + Mathf.Pow(spritea.bounds.center.y - spriteb.bounds.center.y, 2);
        if (distance <= ((spritea.bounds.max.x - spritea.bounds.center.x) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x)) *
            ((spritea.bounds.max.x - spritea.bounds.center.x) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x)))
        {
            return true;
        }
        else { return false; }
    }
    private bool CircleCollision2(Fleer objecta, Wolf objectb)
    {
        SpriteRenderer spritea = objecta.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteb = objectb.GetComponent<SpriteRenderer>();

        float distance = Mathf.Pow(spritea.bounds.center.x - spriteb.bounds.center.x, 2)
            + Mathf.Pow(spritea.bounds.center.y - spriteb.bounds.center.y, 2);
        if (distance <= ((spritea.bounds.max.x - spritea.bounds.center.x) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x)) *
            ((spritea.bounds.max.x - spritea.bounds.center.x) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x)))
        {
            return true;
        }
        else { return false; }
    }
    private bool CircleCollision3(CollisionDetection objecta, PowerUp objectb)
    {
        SpriteRenderer spritea = objecta.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteb = objectb.GetComponent<SpriteRenderer>();

        float distance = Mathf.Pow(spritea.bounds.center.x - spriteb.bounds.center.x, 2)
            + Mathf.Pow(spritea.bounds.center.y - spriteb.bounds.center.y, 2);
        if (distance <= ((spritea.bounds.max.x - spritea.bounds.center.x) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x)) *
            ((spritea.bounds.max.x - spritea.bounds.center.x) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x)))
        {
            return true;
        }
        else { return false; }
    }
    private bool CircleCollision4(CollisionDetection objecta, Wolf objectb)
    {
        SpriteRenderer spritea = objecta.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteb = objectb.GetComponent<SpriteRenderer>();

        float distance = Mathf.Pow(spritea.bounds.center.x - spriteb.bounds.center.x, 2)
            + Mathf.Pow(spritea.bounds.center.y - spriteb.bounds.center.y, 2);
        if (distance <= (((spritea.bounds.max.x - spritea.bounds.center.x) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x)) *
            ((spritea.bounds.max.x - spritea.bounds.center.x) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x)))/1.5)
        {
            return true;
        }
        else { return false; }
    }
    private bool CircleCollision5(CollisionDetection objecta, Obstacle objectb)
    {
        SpriteRenderer spritea = objecta.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteb = objectb.GetComponent<SpriteRenderer>();

        float distance = Mathf.Pow(spritea.bounds.center.x - spriteb.bounds.center.x, 2)
            + Mathf.Pow(spritea.bounds.center.y - spriteb.bounds.center.y, 2);
        if (distance <= ((spritea.bounds.max.x - spritea.bounds.center.x) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x)) *
            ((spritea.bounds.max.x - spritea.bounds.center.x) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x)))
        {
            return true;
        }
        else { return false; }
    }
    private bool Detection(Fleer objecta, Seeker objectb)
    {
        SpriteRenderer spritea = objecta.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteb = objectb.GetComponent<SpriteRenderer>();

        float distance = Mathf.Pow(spritea.bounds.center.x - spriteb.bounds.center.x, 2)
            + Mathf.Pow(spritea.bounds.center.y - spriteb.bounds.center.y, 2);
        if (distance <= ((spritea.bounds.max.x - spritea.bounds.center.x + 1) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x + 1)) *
            ((spritea.bounds.max.x - spritea.bounds.center.x + 1) +
            (spriteb.bounds.max.x - spriteb.bounds.center.x + 1)))
        {
            return true;
        }
        else { return false; }
    }
    void hit(Wolf temp)
    {
        if (bullethit)
        {
            var colorb = temp.GetComponent<Renderer>();
            colorb.material.SetColor("_Color", Color.red);
        }
        else
        {
            var colorb = temp.GetComponent<Renderer>();
            colorb.material.SetColor("_Color", Color.white);
        }
    }
    void ColorSprite(CollisionDetection objecta)
    {
        if (collide)
        {
            var colora = objecta.GetComponent<Renderer>();
            colora.material.SetColor("_Color", Color.gray);
        }
        else
        {
            var colora = objecta.GetComponent<Renderer>();
            colora.material.SetColor("_Color", Color.white);
        }
    }
    void ColorSprite2(Seeker objecta)
    {
        if (seek)
        {
            var colora = objecta.GetComponent<Renderer>();
            colora.material.SetColor("_Color", Color.magenta);
        }
        else
        {
            var colora = objecta.GetComponent<Renderer>();
            colora.material.SetColor("_Color", Color.white);
        }
    }
    void ColorSprite3(Fleer objecta)
    {
        if (seek)
        {
            var colora = objecta.GetComponent<Renderer>();
            colora.material.SetColor("_Color", Color.blue);
        }
        else
        {
            var colora = objecta.GetComponent<Renderer>();
            colora.material.SetColor("_Color", Color.white);
        }
    }
    public void Gameover()
    {
        gameover1.gameObject.SetActive(true);
        instructions.gameObject.SetActive(false);
        time1.text = "" + timer;
    }
    public void Gameover2()
    {
        gameover2.gameObject.SetActive(true);
        instructions.gameObject.SetActive(false);
        time2.text = "" + timer;
    }
}
