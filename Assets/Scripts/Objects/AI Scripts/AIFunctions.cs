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
            timeLeft = 5.0f;
        }
        return timeLeft;
    }
    //Update Chase AI
    public void AIUpdateChase(NavMeshAgent agent, Transform target)
    {
        agent.destination = target.position;
    }

    //Update Search AI


    //--------------------------State Control & Misc.--------------------------------------------
    //Spot player
    public void AISpotPlayer(NavMeshAgent agent, Transform target, string state)
    {
        //if AI spots player in cone of vision
        state = "chase";
    }
    //Lost player
    public void AILostPlayer(NavMeshAgent agent, Transform target, string state)
    {
        //if AI loses player
        state = "search"; //In bounding sphere around current position
    }

}



