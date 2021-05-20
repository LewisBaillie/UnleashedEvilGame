using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIScreamerGuyScript : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    NavMeshAgent agent;
    string state;
    float timeLeft = 5.0f; //TIME IN SECONDS, starts from 5 & counts down

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = "wander";
        agent.GetComponent<AIFunctions>().AIStartWander(agent);
        timeLeft = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "wander")
        {
            agent.GetComponent<AIFunctions>().AIUpdateWander(agent, timeLeft, "wander");
        }
        else if (state == "chase")
        {
            agent.GetComponent<AIFunctions>().AIUpdateChase(agent, target);
        }
        else if (state == "search")
        {
            //Search Update Function
        }
    }
}
