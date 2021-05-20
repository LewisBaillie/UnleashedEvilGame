using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIFunctions : MonoBehaviour
{
    //Wander AI 
   public void AIWander(NavMeshAgent agent)
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
    
    public void AIChase(NavMeshAgent agent, Transform target)
    {
        agent.destination = target.position;
    }
}