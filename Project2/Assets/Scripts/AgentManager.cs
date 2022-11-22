using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField]
    Agent agentPrefab;
    [SerializeField]
    int agentSpawnCount;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    public List<Agent> agents = new List<Agent>();
    public List<Obstacle> obstacles = new List<Obstacle>();
    int itagentindex;
    public int Itagentindex
    {
        get { return itagentindex; }
    }
    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        for(int a = 0; a < agentSpawnCount; a++)
        {
            Vector3 randompos = Vector3.zero;
            randompos.x = Random.Range(-2f, 2f);
            randompos.y = Random.Range(-2f, 2f);
            agents.Add(Instantiate(agentPrefab, randompos, Quaternion.identity));
            agents[a].Init(this);
            //((Tag)agents[a]).ChangeStateTo(TagStates.NotIT);
        }
        //if (agents.Count > 0)
        //{
        //    TagPlayer(0);
        //}
    }
    //public void TagPlayer(int itplayerindex)
    //{
    //    itagentindex = itplayerindex;
    //    ((Tag)agents[itagentindex]).ChangeStateTo(TagStates.It);   
    //}
    //public List<Agent> Agents()
    //{

    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
