using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AINavigationManager : MonoBehaviour
{
    //Surface array
    [SerializeField]
    NavMeshSurface[] floorSurfaces;
    [SerializeField]
    NavMeshSurface ceilingSurface;
    private GameObject Wall;
    NavMeshSurface navSurface;

    private void Awake()
    {
        navSurface = GameObject.FindObjectOfType<NavMeshSurface>();
    }
    public void Regen()
    {
        DestroyNavMesh();
        regenerateFloorNavmesh(floorSurfaces);
        regenerateCeilingNavmesh(ceilingSurface);
    }

    // Start is called before the first frame update
    void Start()
    {
        regenerateFloorNavmesh(floorSurfaces);
        regenerateCeilingNavmesh(ceilingSurface);
    }

    //Regenerate Floor Navmesh
    void regenerateFloorNavmesh(NavMeshSurface[] surfaces)
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

    void DestroyNavMesh()
    {
        NavMesh.RemoveAllNavMeshData();
    }

    //Regenerate Ceiling Navmesh (for Crawler)
    void regenerateCeilingNavmesh(NavMeshSurface surface)
    {
        surface.BuildNavMesh();
    }

    private IEnumerator CheckForRegen()
    {
        while(Wall)
        {
            yield return new WaitForEndOfFrame();
            if(!Wall)
            {
                break;
            }
        }
        Regen();
    }

    public void RegenMesh(GameObject g)
    {
        Wall = g;
        StartCoroutine(CheckForRegen());
    }
}
