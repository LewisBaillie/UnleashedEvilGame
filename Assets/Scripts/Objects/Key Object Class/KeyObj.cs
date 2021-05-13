using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObj : InteractableObj
{
    [SerializeField]
    string _keyName;

    // Start is called before the first frame update
    void Start()
    {
        _objType = ObjectType.KeyObj;
        _type = "Key";

    }

    public string GetName()
    {
        return _keyName;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
