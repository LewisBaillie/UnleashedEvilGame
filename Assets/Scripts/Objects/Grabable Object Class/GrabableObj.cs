using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObj : MoveableObj
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void UpdatePosition()
    {
        if (transform.parent != null)
        {
            transform.position = transform.parent.position;
        }
    }
}
