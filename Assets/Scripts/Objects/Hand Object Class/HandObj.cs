﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandObj : Obj
{
    [Header("Component Settings")]
    [Tooltip("Where the dev assigns the components for the hand to interact with")]
    [SerializeField]
    private PlayerObj _Player;
    [SerializeField]
    private Text _UI;
    [SerializeField]
    private Camera _RayOutput;

    [Header("Hand Settings")]
    [SerializeField]
    private float _ArmLength;
    [SerializeField]
    private GameObject _ObjectInHand;
    [SerializeField]
    private Vector3 _ThrowStengths;
    [SerializeField]
    private Vector3 _HandPosition;
    [SerializeField]
    private KeyCode _PickUpKey;

    // Start is called before the first frame update
    void Start()
    {
        _RayOutput = FindObjectOfType<Camera>();
        _type = "Hand";
        _objType = ObjectType.HandObj;
        _ObjectInHand = null;
    }

    // Update is called once per frame
    void Update()
    {
        HandleGrabingObject();
        HandleHoldingObjects();
    }

    //This function controls what objects the user is handling and this current time.
    private void HandleHoldingObjects()
    {
        if (Input.GetMouseButtonDown(0))
        {

            {
                switch (_ObjectInHand.GetComponent<Obj>().ReturnObjectType())
                {
                    case ObjectType.ThrowingObj:
                        {
                            _ObjectInHand.transform.parent = null;
                            _ObjectInHand.GetComponent<MoveableObj>().SetGravity(true);
                            _ObjectInHand.GetComponent<ThrowingObj>().AddForce(_ThrowStengths);
                            _Player.ReturnInventory().RemoveObject(_ObjectInHand);
                            _ObjectInHand = null;
                            break;
                        }
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _ObjectInHand = _Player.ReturnInventory().GrabObjectFromInvent(0);
            if (_ObjectInHand != null)
            {
                _ObjectInHand.SetActive(true);
                _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _ObjectInHand = _Player.ReturnInventory().GrabObjectFromInvent(1);
            if (_ObjectInHand != null)
            {
                _ObjectInHand.SetActive(true);
                _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _ObjectInHand = _Player.ReturnInventory().GrabObjectFromInvent(2);
            if (_ObjectInHand != null)
            {
                _ObjectInHand.SetActive(true);
                _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _ObjectInHand = _Player.ReturnInventory().GrabObjectFromInvent(3);
            if (_ObjectInHand != null)
            {
                _ObjectInHand.SetActive(true);
                _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _ObjectInHand = _Player.ReturnInventory().GrabObjectFromInvent(4);
            if (_ObjectInHand != null)
            {
                _ObjectInHand.SetActive(true);
                _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _ObjectInHand = _Player.ReturnInventory().GrabObjectFromInvent(5);
            if (_ObjectInHand != null)
            {
                _ObjectInHand.SetActive(true);
                _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _ObjectInHand = _Player.ReturnInventory().GrabObjectFromInvent(6);
            if (_ObjectInHand != null)
            {
                _ObjectInHand.SetActive(true);
                _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _ObjectInHand = _Player.ReturnInventory().GrabObjectFromInvent(7);
            if (_ObjectInHand != null)
            {
                _ObjectInHand.SetActive(true);
                _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _ObjectInHand = _Player.ReturnInventory().GrabObjectFromInvent(8);
            if (_ObjectInHand != null)
            {
                _ObjectInHand.SetActive(true);
                _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _ObjectInHand = _Player.ReturnInventory().GrabObjectFromInvent(9);
            if (_ObjectInHand != null)
            { 
                _ObjectInHand.SetActive(true);
                _ObjectInHand.transform.parent = this.transform;
                _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
            }
        }
    }

    //This function handles objects that are in reach of the object this is attached to and allows that object to pick up other objects
    private void HandleGrabingObject()
    {
        Input.mousePosition.Set(0, 0, 0);
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        //https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
        Ray r = _RayOutput.ScreenPointToRay(Input.mousePosition); //This needs fixing as it isn't always centred
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, _ArmLength, layerMask))
        {
            Debug.Log("Raycast Hit");
            GameObject g = hit.collider.gameObject;
            if (g.GetComponent<Obj>() != null)
            {
                if (!_Player.ReturnInventory().Contains(g))
                {
                    switch (g.GetComponent<Obj>().ReturnObjectType())
                    {
                        default:
                            _UI.enabled = false;
                            break;
                        case 0:
                            {
                                _UI.enabled = false;
                                break;
                            }
                        case ObjectType.InteractableObj:
                            {

                                _UI.enabled = true;
                                _UI.text = "Pick Up " + g.name;
                               if (Input.GetKeyDown(_PickUpKey))
                                {
                                    g.transform.parent = this.transform;
                                    g.GetComponent<ThrowingObj>().AddForce(new Vector3(0, 0, 0));
                                    _Player.GetComponent<PlayerObj>().ReturnInventory().AddObjectToInvent(g);
                                    g.SetActive(false);
                                }
                                break;
                            }
                        case ObjectType.ThrowingObj:
                            {
                                _UI.enabled = true;
                                _UI.text = "Pick Up " + g.name;
                                if (Input.GetKeyDown(_PickUpKey))
                                {
                                    if(_ObjectInHand == null)
                                    {
                                        g.transform.parent = this.transform;
                                        g.GetComponent<ThrowingObj>().AddForce(new Vector3(0, 0, 0));
                                        _Player.GetComponent<PlayerObj>().ReturnInventory().AddObjectToInvent(g);
                                        _ObjectInHand.transform.parent = this.transform;
                                        _ObjectInHand.transform.localPosition = _HandPosition;
                                        _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
                                    }
                                    else
                                    {
                                        g.transform.parent = this.transform;
                                        g.GetComponent<ThrowingObj>().AddForce(new Vector3(0, 0, 0));
                                        _Player.GetComponent<PlayerObj>().ReturnInventory().AddObjectToInvent(g);
                                        //g.SetActive(false);
                                    }
                                }
                                break;
                            }

                    }
                }
                else
                {
                    _UI.enabled = false;
                }
            }
            else
            {
                _UI.enabled = false;
            }
        }    
    }
}
