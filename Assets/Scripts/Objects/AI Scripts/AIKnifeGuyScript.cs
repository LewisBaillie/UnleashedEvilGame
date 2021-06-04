using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIKnifeGuyScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    NavMeshAgent agent;
    string state;
    string area;
    float timeLeft = 5.0f; //TIME IN SECONDS, starts from 5 & counts down
    [SerializeField]
    private GameObject returnTarget;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = "wander";
        agent.GetComponent<AIFunctions>().AIStartWander(agent);
        timeLeft = 6.0f;
        area = "KnifeGuyArea";
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<Death>().playerDie();
        }
        else if (other.tag == "KnifeGuyTetherTrigger")
        {
            area = "KnifeGuyArea";
        }
        else if (other.tag == "OtherTetherTrigger")
        {
            area = "OtherArea";
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (state == "wander")
        {
            agent.speed = 4;
            timeLeft = agent.GetComponent<AIFunctions>().AIUpdateWander(agent, timeLeft, "wander");
            if (area == "otherArea")
            {
                state = "return";
            }
        }
        else if (state == "chase")
        {
            agent.speed = 8;
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
