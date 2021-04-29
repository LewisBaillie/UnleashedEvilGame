using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : MoveableObj
{
    private HandObj _Hand;
    private Inventory _Inventory;

    virtual public void SetUpComponent()
    {
        _objType = ObjectType.PlayerObj;
        _type = "Player";
    }

    void Start()
    {
        _Inventory = new Inventory();
        SetUpComponent();
    }

    void Update()
    {
        CalculateMovement();
        HieghtMaipulation();
    }

    public Inventory ReturnInventory()
    {
        return _Inventory;
    }
}
