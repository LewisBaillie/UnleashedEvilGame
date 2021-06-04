using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIFunctions : MonoBehaviour
{
//-----------------------------START----------------------------------------
    //Start Wander AI 
   public void AIStartWander(NavMeshAgent agent)
    {
        Vector3 position = GetRandomPoint(agent, 25f);
        agent.destination = position;
    }

    //Get random point on the navmesh https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html
    public Vector3 GetRandomPoint(NavMeshAgent agent, float walkRadius)
    {
        Vector3 randomPos = Random.insideUnitSphere * walkRadius;

        randomPos += agent.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;
        return finalPosition;
    }
    //Start Chase AI Not Necessary

    //Start Search AI


//----------------------------UPDATE----------------------------------------
    //Update Wander AI
    public float AIUpdateWander(NavMeshAgent agent, float timeLeft, string state)
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            agent.GetComponent<AIFunctions>().AIStartWander(agent);
            if (agent.tag == "BigMonster")
            {
                timeLeft = 12.0f;
            }
            else if (agent.tag == "KnifeMonster")
            {
                timeLeft = 8.0f;
            }
            else if (agent.tag == "CrawlerMonster")
            {
                timeLeft = 15.0f;
            }
            else if (agent.tag == "ScreamerMonster")
            {
                timeLeft = 25.0f;
            }
        }
        return timeLeft;
    }
    //Update Chase AI
    public void AIUpdateChase(NavMeshAgent agent, Transform target)
    {
        agent.destination = target.position;
    }

    //Update Search AI


    //--------------------------State Control & Misc.-------------------------
    //Spot player
    public void AISpotPlayer(NavMeshAgent agent, Transform target, ref string state)
    {
        //if AI spots player in cone of vision
        state = "chase";
    }
    //Lost player
    public void AILostPlayer(NavMeshAgent agent, Transform target, ref string state)
    {
        //if AI loses player
        state = "search"; //In bounding sphere around current position
    }

    //--------------------------Detection------------------------------------
    public void AIDetectPlayer(NavMeshAgent agent, Transform target, ref string state, float x_distance, float z_distance, float totalDistance)
    {
        x_distance = agent.transform.position.x - target.position.x;
        z_distance = agent.transform.position.z - target.position.z;
        totalDistance = Mathf.Sqrt((x_distance * x_distance) + (z_distance * z_distance));
        if (totalDistance <= 30.0f)
        {
            state = "chase";
        }

    }
}



