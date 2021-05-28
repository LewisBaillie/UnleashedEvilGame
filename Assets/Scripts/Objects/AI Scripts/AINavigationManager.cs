using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigationManager : MonoBehaviour
{
    public NavMeshSurface[] surfaces;
    public void regenerateNavmesh(float timer)
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        regenerateNavmesh(0);
    }
}
