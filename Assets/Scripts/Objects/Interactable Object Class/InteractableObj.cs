using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : Object
{
    // Start is called before the first frame update
    void Start()
    {
        _objType = ObjectType.InteractableObj;
        _type = "Cube";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
