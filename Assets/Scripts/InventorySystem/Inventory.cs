using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;
using System.Runtime.Serialization;

/// <summary>
/// A basic inventory system that can add and remove objects in a concurrent or linear fashion
/// </summary>
/// 

public class Inventory
{
    private GameObject[] _Inventory;
    private int _ActivePosition = 0;

    public Inventory()
    {
        NullCheckInventory();
    }

    private void NullCheckInventory()
    {
        if (_Inventory == null)
        {
            _Inventory = new GameObject[10];
            _ActivePosition = 0;
        }
    }

    public int ActivePosition()
    {
        return _ActivePosition;
    }

    public void AddObjectToInvent(GameObject go)
    {
        for (int i = 0; i < _Inventory.Length; i++)
        {
            if(_Inventory[i] == null)
            {
                _Inventory[i] = go;
                _ActivePosition = i;
                break;
            }
        }
    }

    //remove an object from the inventory
    public void RemoveObject(GameObject go)
    {
        for (int i = 0; i < _Inventory.Length; ++i)
        {
            if (_Inventory[i] == go)
            {
                _Inventory[i] = null;
                break;
            }
        }
    }

    public bool Contains(GameObject g)
    {
        foreach (var item in _Inventory)
        {
            if (item == g)
            {
                return true;
            }
        }
        return false;
    }

    //Returns a specfied object from the inventory
    public GameObject GrabObjectFromInvent(int Place)
    {
        //_ActivePosition = Place;    
        return _Inventory[Place];
    }

    public void SetInventoryPlace(int place)
    {
        _ActivePosition = place;
    }

    public GameObject GetCurrentObject()
    {
        return _Inventory[_ActivePosition];
    }
}
