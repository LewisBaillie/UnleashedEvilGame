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
    private Text _Hotbar;
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
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Locked;
        _Hotbar.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleGrabingObject();
        HandleHoldingObjects();
        HandleHotbar();
    }

    //This function controls what objects the user is handling and this current time.
    private void HandleHoldingObjects()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(_ObjectInHand != null)
            {
                switch (_ObjectInHand.GetComponent<Obj>().ReturnObjectType())
                {
                    case ObjectType.ThrowingObj:
                        {
                            _ObjectInHand.transform.localRotation = Quaternion.Euler(0, 0, 0);
                            _ObjectInHand.transform.parent = null;
                            _ObjectInHand.GetComponent<MoveableObj>().SetGravity(true);
                            _ObjectInHand.GetComponent<ThrowingObj>().IsInHand(false);
                            _ObjectInHand.GetComponent<ThrowingObj>().AddForce(_ThrowStengths);
                            _Player.ReturnInventory().RemoveObject(_ObjectInHand);
                            _ObjectInHand = null;
                            break;
                        }
                    case ObjectType.TorchObj:
                        {
                            _ObjectInHand.GetComponent<TorchObj>().ItemAction();
                            break;
                        }
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HandleInventoryCall(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HandleInventoryCall(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HandleInventoryCall(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HandleInventoryCall(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            HandleInventoryCall(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            HandleInventoryCall(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            HandleInventoryCall(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            HandleInventoryCall(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            HandleInventoryCall(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            HandleInventoryCall(9);
        }
    }

    //This function handles objects that are in reach of the object this is attached to and allows that object to pick up other objects
    private void HandleGrabingObject()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        //https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
        Ray r = _RayOutput.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, _ArmLength, layerMask))
        {
            GameObject g = hit.collider.gameObject;
            if (g.GetComponent<Obj>() != null)
            {
                if (!_Player.ReturnInventory().Contains(g))
                {
                    switch (g.GetComponent<Obj>().ReturnObjectType())
                    {
                        default:
                        case 0:
                            {
                                _UI.enabled = false;
                                break;
                            }
                        case ObjectType.KeyObj:
                        case ObjectType.TorchObj:
                            {
                                _UI.enabled = true;
                                _UI.text = "Pick Up " + g.name;
                                if (Input.GetKeyDown(_PickUpKey) && g.GetComponent<InteractableObj>().CanObjectBePickedUp())
                                {
                                    if (_ObjectInHand == null)
                                    {
                                        // May not be 0 in the future
                                        _Player.GetComponent<PlayerObj>().ReturnInventory().AddObjectToInvent(g);
                                        g.transform.parent = this.transform.GetChild(0);
                                        g.transform.localPosition = _HandPosition;
                                        _ObjectInHand = g;
                                        g.transform.rotation = this.transform.GetChild(0).rotation;
                                    }
                                    else
                                    {
                                        g.transform.parent = this.transform;
                                        _Player.GetComponent<PlayerObj>().ReturnInventory().AddObjectToInvent(g);
                                        g.SetActive(false);
                                    }
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
                                        _ObjectInHand = g;
                                        g.transform.parent = this.transform.GetChild(0);
                                        g.GetComponent<ThrowingObj>().AddForce(new Vector3(0, 0, 0));
                                        _Player.GetComponent<PlayerObj>().ReturnInventory().AddObjectToInvent(g);
                                        g.GetComponent<ThrowingObj>().IsInHand(true);
                                        g.transform.localPosition = _HandPosition;
                                        g.GetComponent<MoveableObj>().SetGravity(false);
                                        _ObjectInHand.transform.localRotation = Quaternion.Euler(0, 0, 0);
                                    }
                                    else
                                    {
                                        g.transform.parent = this.transform.GetChild(0);
                                        _ObjectInHand.transform.localRotation = Quaternion.Euler(0, 0, 0);
                                        g.GetComponent<ThrowingObj>().AddForce(new Vector3(0, 0, 0));
                                        _Player.GetComponent<PlayerObj>().ReturnInventory().AddObjectToInvent(g);
                                        g.SetActive(false);
                                    }
                                }
                                break;
                            }
                        case ObjectType.DoorObj:
                            {
                                if (_ObjectInHand != null && _ObjectInHand.GetComponent<Obj>().ReturnObjectType() == ObjectType.KeyObj)
                                { 
                                    string keyName = _ObjectInHand.GetComponent<KeyObj>().GetName();
                                    if (g.GetComponent<DoorObj>().IsDoorUnlockable(keyName))
                                    {
                                        _UI.enabled = true;
                                        _UI.text = "Unlock " + keyName + " Door";
                                        if (Input.GetKeyDown(_PickUpKey))
                                        {
                                            _Player.ReturnInventory().RemoveObject(_ObjectInHand);
                                            Destroy(_ObjectInHand);
                                            _ObjectInHand = null;
                                            g.SetActive(false);
                                        }
                                    }
                                    else
                                    {
                                        _UI.enabled = true;
                                        _UI.text = "You need the " + g.GetComponent<DoorObj>().GetName() + " key for this door";
                                    }
                                }
                                else
                                {
                                    _UI.enabled = true;
                                    _UI.text = "You need the " + g.GetComponent<DoorObj>().GetName() + " key for this door";
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

    private void HandleInventoryCall(int pos)
    {
        if (_ObjectInHand != null)
        {
            _ObjectInHand.SetActive(false);
        }
        _ObjectInHand = _Player.ReturnInventory().GrabObjectFromInvent(pos);
        if(_ObjectInHand != null)
        {
            _ObjectInHand.SetActive(true);
            switch (_ObjectInHand.GetComponent<Obj>().ReturnObjectType())
            {
                default:
                    {
                        _ObjectInHand.transform.parent = this.transform.GetChild(0);
                        _ObjectInHand.transform.localPosition = _HandPosition;
                        _ObjectInHand.transform.rotation = this.transform.GetChild(0).rotation;
                        break;
                    }   
                case ObjectType.ThrowingObj:
                    {
                        _ObjectInHand.GetComponent<ThrowingObj>().AddForce(new Vector3(0, 0, 0));
                        _ObjectInHand.GetComponent<ThrowingObj>().IsInHand(true);
                        _ObjectInHand.GetComponent<MoveableObj>().SetGravity(false);
                        break;
                    }
            }
        }
    }

    private void HandleHotbar()
    {
        _Hotbar.text = "";
        for (int i = 0; i < 10; ++i)
        {
            if(_Player.ReturnInventory().GrabObjectFromInvent(i) != null)
            {
                _Hotbar.text += (i+1) + ": " + _Player.ReturnInventory().GrabObjectFromInvent(i).name + "    ";
            }
        }
    }
}
