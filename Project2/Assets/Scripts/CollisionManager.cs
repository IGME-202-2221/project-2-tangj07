using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour
{
    //everything manager
    public CollisionDetection player;
    public bool bullethit = false, dead, ate = false, pick = false, spawn = false;
    public Shooting[] bullets;
    public Wolf[] wolves;
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
        bullets = GameObject.FindObjectsOfType<Shooting>();
        wolves = GameObject.FindObjectsOfType<Wolf>();
        followers = GameObject.FindObjectsOfType<Fleer>();
        powerUp = GameObject.FindObjectsOfType<PowerUp>();

        foreach (Wolf temp in wolves)
        {
            foreach (Shooting temp2 in bullets)
            {
                bullethit = CircleCollision(temp, temp2);
                hit(temp);
                if (bullethit) { temp.hp -= 1; break; }
            }
        }
        foreach (Wolf temp in wolves)
        {
            foreach (Fleer temp2 in followers)
            {
                temp2.dead = CircleCollision2(temp, temp2);
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
    private bool CircleCollision2(Wolf objecta, Fleer objectb)
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
