using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TorchObj : InteractableObj 
{
    [Header("Torch Settings")]
    [SerializeField]
    Light _Bulb;

    private void Start()
    {
        _type = "Torch";
        _objType = ObjectType.TorchObj;
    }

    public void ItemAction()
    {
        if (!_Bulb.gameObject.activeSelf)
        {
            _Bulb.gameObject.SetActive(true);
        }
        else
        {
            _Bulb.gameObject.SetActive(false);
        }
    }

}
