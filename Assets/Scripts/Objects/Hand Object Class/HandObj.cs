using System.Collections;
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
    [SerializeField]
    private Sound _Sounds;

    [Header("Hand Settings")]
    [SerializeField]
    private float _ArmLength;
    private GameObject _ObjectInHand;
    [SerializeField]
    private Vector3 _ThrowStengths;
    [SerializeField]
    private KeyCode _PickUpKey;                                                                                            

    [SerializeField]
    private List<GameObject> _AllObjects;

    bool DeleteOnNextAction;

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
        foreach (GameObject item in _AllObjects)
        {
            if (item.name == "Torch")
            {
                item.SetActive(true);
                _ObjectInHand = item;
                _Player.GetComponent<PlayerObj>().ReturnInventory().AddObjectToInvent(item);
            }
        }
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
                            _Sounds.PlaySound(SoundEffect.throwingSound);
                            foreach (GameObject item in _AllObjects)
                            {
                                if (item.name == "Rock")
                                {
                                    _ObjectInHand = Instantiate(item, this.transform.GetChild(0));
                                    item.SetActive(false);
                                    break;
                                }
                            }
                            _ObjectInHand.transform.parent = null;                            
                            _ObjectInHand.GetComponent<Rigidbody>().isKinematic = false;
                            _ObjectInHand.GetComponent<Rigidbody>().useGravity = true;
                            _ObjectInHand.GetComponent<ThrowingObj>().SetIsThrown(true);
                            _ObjectInHand.GetComponent<ThrowingObj>().AddForce(_ThrowStengths);      
                            _Player.ReturnInventory().RemoveObject(_Player.ReturnInventory().GetCurrentObject()); 
                            _ObjectInHand = null;
                            break;
                        }
                    case ObjectType.TorchObj:
                        {
                            _Sounds.PlaySound(SoundEffect.torchSound);
                            _ObjectInHand.GetComponent<TorchObj>().ItemAction();
                            break;
                        }
                }

            }
        }
        for (int i = 0; i < 9; ++i)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                HandleInventoryCall(i);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
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
            if(g.tag == "The Bean")
            {
                if (_ObjectInHand != null)
                {
                    if (_ObjectInHand.GetComponent<TorchObj>())
                    {
                        if (DeleteOnNextAction)
                        {
                            Destroy(g);
                        }
                        if (_ObjectInHand.GetComponent<TorchObj>().IsTorchOn())
                        {
                            DeleteOnNextAction = true;
                        }
                    }
                }
            }
            if (g.GetComponent<Obj>() != null)
            {
                if (!_Player.ReturnInventory().Contains(g))
                {
                    switch (g.GetComponent<Obj>().ReturnObjectType())
                    {
                        default:
                            _UI.enabled = false;
                            break;
                        case ObjectType.TorchObj:
                            {
                                _UI.enabled = true;
                                _UI.text = "Pick Up " + g.name;
                                if (Input.GetKeyDown(_PickUpKey) && g.GetComponent<InteractableObj>().CanObjectBePickedUp())
                                {
                                    foreach (GameObject item in _AllObjects)
                                    {
                                        if (item.name == "Torch")
                                        {
                                            _Sounds.PlaySound(SoundEffect.pickupSound);
                                            Destroy(g);
                                            item.SetActive(true);
                                            _ObjectInHand = item;
                                            _Player.GetComponent<PlayerObj>().ReturnInventory().AddObjectToInvent(item);
                                        }
                                        else
                                        {
                                            item.SetActive(false);
                                        }
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
                                    foreach (GameObject item in _AllObjects)
                                    {
                                        if (item.name == "Rock")
                                        {
                                            _Sounds.PlaySound(SoundEffect.pickupSound);
                                            Destroy(g);
                                            item.SetActive(true);
                                            _ObjectInHand = item;
                                            _Player.GetComponent<PlayerObj>().ReturnInventory().AddObjectToInvent(item);
                                        }
                                        else
                                        {
                                            item.SetActive(false);
                                        }
                                    }
                                    _Player.GetComponent<PlayerObj>().NewObjectInHand();
                                }
                                break;
                            }
                        case ObjectType.KeyObj:
                            {
                                _UI.enabled = true;
                                _UI.text = "Pick Up " + g.name;
                                if (Input.GetKeyDown(_PickUpKey))
                                {
                                    foreach (GameObject item in _AllObjects)
                                    {
                                        if (item.name == "Key")
                                        {
                                            _Sounds.PlaySound(SoundEffect.pickupSound);
                                            item.SetActive(true);
                                            _ObjectInHand = item;
                                            _Player.GetComponent<PlayerObj>().ReturnInventory().AddObjectToInvent(g);
                                            g.SetActive(false);
                                        }
                                        else
                                        {
                                            item.SetActive(false);
                                        }
                                    }
                                    _Player.GetComponent<PlayerObj>().NewObjectInHand();
                                }
                                break;
                            }
                        case ObjectType.DoorObj:
                            {
                                if (_ObjectInHand != null && _ObjectInHand.GetComponent<Obj>().ReturnObjectType() == ObjectType.KeyObj)
                                {
                                    string keyName = _Player.ReturnInventory().GetCurrentObject().GetComponent<KeyObj>().GetName();
                                    if (g.GetComponent<DoorObj>().IsDoorUnlockable(keyName))
                                    {
                                        _UI.enabled = true;
                                        _UI.text = "Unlock " + keyName + " Door";
                                        if (Input.GetKeyDown(_PickUpKey))
                                        {
                                            _Sounds.PlaySound(SoundEffect.doorSound);
                                            _Player.ReturnInventory().RemoveObject(_Player.ReturnInventory().GetCurrentObject());
                                            _ObjectInHand.SetActive(false);
                                            _ObjectInHand = null;
                                            g.SetActive(false);
                                        }
                                    }
                                    else
                                    {
                                        _UI.enabled = true;
                                        _UI.text = "You need the " + g.GetComponent<DoorObj>().GetName() + " key for this door";
                                    }
                                    _Player.GetComponent<PlayerObj>().NewObjectInHand();
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
        if(_Player.ReturnInventory().GrabObjectFromInvent(pos) != null)
        {
            string viewmodelName = "temp";
            switch (_Player.ReturnInventory().GrabObjectFromInvent(pos).GetComponent<Obj>().ReturnObjectType())
            {
                case ObjectType.TorchObj:
                    {
                        viewmodelName = "Torch";
                        break;
                    }
                case ObjectType.ThrowingObj:
                    {
                        viewmodelName = "Rock";
                        break;
                    }
                case ObjectType.KeyObj:
                    {
                        viewmodelName = "Key";
                        break;
                    }
            }
            foreach (GameObject item in _AllObjects)
            {
                if (item.name == viewmodelName)
                {
                    _ObjectInHand = item;
                    break;
                }
            }
        }
        else
        {
            _ObjectInHand = null;
        }
        _Player.ReturnInventory().SetInventoryPlace(pos);
        if (_ObjectInHand != null)
        {
            _ObjectInHand.SetActive(true);
            switch (_ObjectInHand.GetComponent<Obj>().ReturnObjectType())
            {
                default:
                    break;
                case ObjectType.ThrowingObj:
                    {
                        _ObjectInHand.GetComponent<ThrowingObj>().AddForce(new Vector3(0, 0, 0));
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
            if (_Player.ReturnInventory().GrabObjectFromInvent(i) != null)
            {
                _Hotbar.text += (i + 1) + ": " + _Player.ReturnInventory().GrabObjectFromInvent(i).name + "    ";
            }
        }
    }

}
