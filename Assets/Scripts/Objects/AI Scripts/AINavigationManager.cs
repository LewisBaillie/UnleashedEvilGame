using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AINavigationManager : MonoBehaviour
{
    //Surface array
    NavMeshSurface[] floorSurfaces;
    NavMeshSurface ceilingSurface;

    public void Regen()
    {
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
    //Regenerate Ceiling Navmesh (for Crawler)
    void regenerateCeilingNavmesh(NavMeshSurface surface)
    {
        surface.BuildNavMesh();
    }
}
