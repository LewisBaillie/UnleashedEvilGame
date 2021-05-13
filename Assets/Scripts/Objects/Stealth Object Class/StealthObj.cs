using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthObj : Obj
{
    private GameObject _Border;


    [SerializeField]
    private GameObject _NonParent;
    [SerializeField]
    private List<GameObject> _Accessories;

    // Start is called before the first frame update
    void Start()
    {
        SetObjectType(ObjectType.StealthObj);
        if(transform.parent == null || _NonParent == transform.parent.gameObject)
        {
            _Border = new GameObject(this.name + " border");
            _Border.transform.position = this.transform.position;
            _Border.transform.parent = gameObject.transform;
            _Border.AddComponent<StealthObj>();
            _Border.AddComponent<BoxCollider>();
            _Border.GetComponent<BoxCollider>().isTrigger = true;
            _Border.GetComponent<StealthObj>().SetUpBorder();
            _Border.GetComponent<StealthObj>()._Accessories.Add(this.transform.gameObject);
            foreach (GameObject obj in _Accessories)
            {
                _Border.GetComponent<StealthObj>()._Accessories.Add(obj);
            }
            Destroy(this);
        }
    }

    //Sets the size of a box collider surrounding the object
    private void SetUpBorder()
    {
        _Accessories = new List<GameObject>();
        gameObject.GetComponent<BoxCollider>().size += new Vector3(2 + transform.parent.localScale.x, transform.parent.localScale.y, 2 + transform.parent.localScale.z);
    }
    //Restricts the player from standing near the object
    void OnTriggerEnter(Collider other)
    {
        GameObject g = other.gameObject;
        if (g.GetComponent<Obj>() != null)
        {
            switch (g.GetComponent<Obj>().ReturnObjectType())
            {
                case ObjectType.PlayerObj:
                    {
                        g.GetComponent<PlayerObj>().RestrictStanding(true);
                        break;
                    }
            
            }
        }
    }
    //Checks to see if the player is crouched and if so turns off the objects box collider
    void OnTriggerStay(Collider other)
    {
        GameObject g = other.gameObject;
        if (g.GetComponent<Obj>() != null)
        {
            switch (g.GetComponent<Obj>().ReturnObjectType())
            {
                case ObjectType.PlayerObj:
                    {
                        if (g.GetComponent<PlayerObj>().IsStanding())
                        {
                            foreach (GameObject obj in _Accessories)
                            {
                                if(obj != null)
                                {
                                    _Accessories.Remove(obj);
                                }
                                if (obj.GetComponent<BoxCollider>())
                                {
                                    obj.GetComponent<BoxCollider>().enabled = true;
                                }
                                if (obj.GetComponent<SphereCollider>())
                                {
                                    obj.GetComponent<SphereCollider>().enabled = true;
                                }
                                if (obj.GetComponent<Rigidbody>())
                                {
                                    obj.GetComponent<Rigidbody>().useGravity = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (GameObject obj in _Accessories)
                            {
                                if (obj.GetComponent<Rigidbody>())
                                {
                                    obj.GetComponent<Rigidbody>().useGravity = false;
                                }
                                if (obj.GetComponent<BoxCollider>())
                                {
                                    obj.GetComponent<BoxCollider>().enabled = false;
                                }
                                if (obj.GetComponent<SphereCollider>())
                                {
                                    obj.GetComponent<SphereCollider>().enabled = false;
                                }
                            }
                        }
                        break;
                    }

            }
        }
    }
    //This resets box colliders back to the original on state when the player leaves the objects vicinity
    void OnTriggerExit(Collider other)
    {
        GameObject g = other.gameObject;
        if (g.GetComponent<Obj>() != null)
        {
           if (g.GetComponent<Obj>().ReturnObjectType() == ObjectType.PlayerObj)
            {
                g.GetComponent<PlayerObj>().RestrictStanding(false);
                foreach (GameObject obj in _Accessories)
                {
                    if (obj.GetComponent<BoxCollider>())
                    {
                        obj.GetComponent<BoxCollider>().enabled = true;
                    }
                    if (obj.GetComponent<SphereCollider>())
                    {
                        obj.GetComponent<SphereCollider>().enabled = true;
                    }
                    if (obj.GetComponent<Rigidbody>())
                    {
                        obj.GetComponent<Rigidbody>().useGravity = true;
                    }
                }
            }
                
        }
    }
}
