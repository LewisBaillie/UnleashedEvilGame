using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgentScript : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    //Get random point on the navmesh https://gist.github.com/IJEMIN/f2510a85b1aaf3517da1af7a6f9f1ed3
    static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);
        return hit.position;
    }

    //Wander AI 
    void AIWander()
    {
        agent.destination = GetRandomPoint(agent.gameObject.transform.position, NavMesh.AllAreas);
    }

    void AIChase()
    {
        agent.destination = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        AIChase();
    }
}
