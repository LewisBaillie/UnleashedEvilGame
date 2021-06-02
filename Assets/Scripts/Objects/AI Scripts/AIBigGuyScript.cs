﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIBigGuyScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    NavMeshAgent agent;
    string state;
    float timeLeft = 5.0f; //TIME IN SECONDS, starts from 5 & counts down
    [SerializeField]
    private GameObject m_AINavigationManager;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = "chase";
        agent.GetComponent<AIFunctions>().AIStartWander(agent);
        timeLeft = 6.0f;
    }

    void Charge(GameObject destructibleWall)
    {
        Destroy(destructibleWall);
        m_AINavigationManager.GetComponent<AINavigationManager>().Regen();
    }
    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "DestructibleWall")
        if (other.gameObject.name == "WallDestructiblePrefab")
        {
            if (state == "chase")
            {
                Charge(other.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "wander")
        {
            timeLeft = agent.GetComponent<AIFunctions>().AIUpdateWander(agent, timeLeft, "wander");
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
