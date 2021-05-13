using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingObj : Obj
{
    void Start()
    {
        _objType = ObjectType.ThrowingObj;
        _type = "";
    }
    public void AddForce(Vector3 Force)
    {
        GetComponent<Rigidbody>().AddForce(transform.TransformVector(Force), ForceMode.Impulse);
    }
}
