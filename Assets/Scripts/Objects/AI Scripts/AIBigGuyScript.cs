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
    string area;
    float timeLeft = 5.0f; //TIME IN SECONDS, starts from 5 & counts down
    [SerializeField]
    private GameObject m_AINavigationManager;
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
        player = GameObject.FindGameObjectWithTag("Player");
        area = "BigGuyArea";
    }

    void Charge(GameObject destructibleWall)
    {
        m_AINavigationManager.GetComponent<AINavigationManager>().RegenMesh(destructibleWall);
        Destroy(destructibleWall);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DestructibleWall")
        {
            if (state == "chase")
            {
                Charge(other.gameObject);
            }
        }
        else if(other.tag == "Player")
        {
            player.GetComponent<Death>().playerDie();
        }
        else if (other.tag == "BigGuyTetherTrigger")
        {
            area = "BigGuyArea";
        }
        else if (other.tag == "OtherTetherTrigger")
        {
            area = "otherArea";
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
        }
        else if (state == "chase")
        {
            agent.speed = 5;
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
