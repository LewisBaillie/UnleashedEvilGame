using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingObj : GrabableObj
{

    [Header("Throwing Settings")]
    [Tooltip("Controls factors to do with throwing")]
    [SerializeField]
    private float _Dampener;
    [SerializeField]
    private GameObject _Player;
    

    void Start()
    {
        _objType = ObjectType.ThrowingObj;
        _type = "";
        if(ReturnSpeed() == 0)
        {
            Debug.LogWarning("Make sure that if you throw" + name + " you set the speed to one");
        }
    }

    private Vector3 _ActiveForce;
    // Update is called once per frame
    void Update()
    {
        if (!_InHand)
        {
            _ActiveForce = _ActiveForce * _Dampener;
            CalculateMovement(_ActiveForce);
        }
    }

    public void AddForce(Vector3 Force)
    {
        _ActiveForce = Force;
    }
}
