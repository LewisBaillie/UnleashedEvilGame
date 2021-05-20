using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIAgentScript : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    NavMeshAgent agent;
    string state;
    float timeLeft; //TIME IN SECONDS

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = "wander";
        agent.GetComponent<AIFunctions>().AIWander(agent);
        timeLeft = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Time
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f && state == "wander")
        {
            agent.GetComponent<AIFunctions>().AIWander(agent);
            timeLeft = 6.0f;
        }

    }
}
