using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for objects that can be carried.
/// </summary>

public class GrabableObj : MoveableObj
{
    [Header("In Hand Settings")]
    [Tooltip("Controls factors to do with Objects in the hand")]
    [SerializeField]
    protected bool _InHand;

    //Lets you set if the object is in a hand or not
    public void IsInHand(bool val)
    {
        _InHand = val;
    }
    //returns if the object is in hand
    public bool IsInHand()
    {
        return _InHand;
    }
}
