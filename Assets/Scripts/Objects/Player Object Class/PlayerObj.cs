using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        CalculateLean();
        //To be Removed
        if(Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("NewTestScene");
        }
    }

    public Inventory ReturnInventory()
    {
        return _Inventory;
    }
}
