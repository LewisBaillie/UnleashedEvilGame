using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : Obj
{
    [SerializeField]
    private bool _canBePickedUp;


    // Start is called before the first frame update
    void Start()
    {
        _objType = ObjectType.InteractableObj;
        _type = "Cube";
    }

    public bool CanObjectBePickedUp()
    {
        return _canBePickedUp;
    }

}
