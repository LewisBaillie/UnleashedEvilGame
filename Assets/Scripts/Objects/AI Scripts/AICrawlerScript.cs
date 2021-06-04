using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AICrawlerScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    NavMeshAgent agent;
    string state;
    string area;
    float timeLeft = 5.0f; //TIME IN SECONDS, starts from 5 & counts down
    [SerializeField]
    private GameObject returnTarget;

    private float m_xDistance;
    private float m_zDistance;
    private float m_hypotenuseDistance;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = "wander";
        agent.GetComponent<AIFunctions>().AIStartWander(agent);
        timeLeft = 6.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        area = "CrawlerArea";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CrawlerTetherTrigger")
        {
            area = "CrawlerArea";
        }
        else if (other.tag == "OtherTetherTrigger")
        {
            area = "OtherArea";
        }
        else if (other.tag == "Player")
        {
            player.GetComponent<Death>().playerDie();
        }   
    }
    // Update is called once per frame
    void Update()
    {
        if (state == "wander")
        {
            agent.speed = 2;
            timeLeft = agent.GetComponent<AIFunctions>().AIUpdateWander(agent, timeLeft, "wander");
            if (area == "otherArea")
            {
                state = "return";
            }
            //agent.GetComponent<AIFunctions>().AIDetectPlayer(agent, target, ref state, m_xDistance, m_zDistance, m_hypotenuseDistance);
        }
        else if (state == "chase")
        {
            agent.speed = 6;
            agent.GetComponent<AIFunctions>().AIUpdateChase(agent, target);
        }
        else if (state == "return")
        {
            target = returnTarget.transform;
            if (agent.transform == returnTarget.transform)
            {
                state = "wander";
            }
        }
    }
}
