using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a identifier for GameObject children that can be used for casting.
/// </summary>
public enum ObjectType
{ 
    nullObj,
    PlayerObj,
    MoveableObj,
    HandObj,
    StealthObj,
    InteractableObj,
    ThrowingObj,
    TorchObj,

};

/// <summary>
/// Head class for objects that are used in gameplay. Just contains the identifier for the name of the object and its name
/// </summary>
public class Obj : MonoBehaviour
{
    protected ObjectType _objType;
    protected string _type;

    virtual public void SetUpComponent()
    {
        _objType = ObjectType.nullObj;
        _type = "";
    }

    //Sets the objects type, make sure it matches the actual object tupe
    public void SetObjectType(ObjectType objectType)
    {
        _objType = objectType;
    }
    //Sets the type, usually just for storing object name
    public void SetType(string Type)
    {
        _type = Type;
    }
    //Return the objects type.
    public ObjectType ReturnObjectType()
    {
        return _objType;
    }
    //Return the type.
    public string ReturnType()
    {
        return _type;
    }

    //A common element so that class type for interactional objects doesn't have to be diserned
    virtual public void ItemAction()
    {
        Debug.Log("Action Called");
    }

}
