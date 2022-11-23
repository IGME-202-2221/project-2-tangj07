using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour
{
    //everything manager
    public CollisionDetection player;
    public bool bullethit = false, dead, ate = false, pick = false, spawn = false, wander;
    public Shooting[] bullets;
    public Wolf[] wolves;
    public Seeker[] wolves2;
    public Fleer[] followers;
    public PowerUp[] powerUp;
    [SerializeField] 
    Text gameover;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        spawn = false;
        wolves2 = FindObjectsOfType<Seeker>();
        bullets = FindObjectsOfType<Shooting>();
        wolves = FindObjectsOfType<Wolf>();
        followers = FindObjectsOfType<Fleer>();
        powerUp = FindObjectsOfType<PowerUp>();

        foreach (Wolf temp in wolves)
        {
            if(CircleCollision4(player, temp)) { dead = true; Gameover(); }
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
                if(Detection(followers[a], wolves2[b])) 
                {
                    wolves2[b].wander = false;
                    followers[a].follow = false;
                    wolves2[b].assignedprey = followers[a];
                    followers[a].predator = wolves2[b];
                }
                else if(!Detection(followers[a], wolves2[b]))
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
                if(CircleCollision2(followers[a], wolves[b])) { followers[a].dead = true; }
            }
        }
        foreach (PowerUp temp in powerUp)
        {
            temp.pick = CircleCollision3(player, temp);
            if (temp.pick) { spawn = true; }
        }
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
        if (distance <= ((spritea.bounds.max.x - spritea.bounds.center.x+1) +
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
    public void Gameover()
    {
        gameover.gameObject.SetActive(true);
    }
}
