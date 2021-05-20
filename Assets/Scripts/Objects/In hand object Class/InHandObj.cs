using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHandObj : Obj
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnd(Collider other)
    {
        GetComponent<MeshRenderer>().enabled = true;
    }
}
