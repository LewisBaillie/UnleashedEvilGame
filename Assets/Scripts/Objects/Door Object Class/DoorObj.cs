using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObj : Obj
{
    [SerializeField]
    string _doorName;

    // Start is called before the first frame update
    void Start()
    {
        _objType = ObjectType.DoorObj;
        _type = "Door";
    }

    public bool IsDoorUnlockable(string keyName)
    {
        return (keyName == _doorName);
    }

    public string GetName()
    {
        return _doorName;
    }
}
