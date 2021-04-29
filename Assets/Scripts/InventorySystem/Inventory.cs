using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// A basic inventory system that can add and remove objects in a concurrent or linear fashion
/// </summary>
public class Inventory
{
    ConcurrentDictionary<int, InventoryItem> _Inventory;

    public Inventory()
    {
        NullCheckInventory();
    }

    /// <summary>
    /// A container for storing multiple objects in one place. This container also includes functions to help take out and remove objects
    /// </summary>
    public struct InventoryItem
    {
        public bool _AddedSucessfully;
        private string _Tag;
        private List<GameObject> _Objects;
        public InventoryItem(GameObject go, string Tag)
        {
            _Objects = new List<GameObject>();
            _Tag = Tag;
            _Objects.Add(go);
            _AddedSucessfully = false;
        }
        public InventoryItem(GameObject go)
        {
            _Objects = new List<GameObject>();
            _Tag = go.tag;
            _Objects.Add(go);
            _AddedSucessfully = false;
        }
        //Checks if an object can be added to this items list
        private bool ObjectIsEigable(GameObject g)
        {
            if (g.tag == _Tag)
            {
                _AddedSucessfully = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ObjectIsEigable(GameObject g, string tag)
        {
            if (tag == _Tag)
            {
                _AddedSucessfully = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Adds a Unity GameObject to a list
        public bool AddObject(GameObject g)
        {
            if (ObjectIsEigable(g) && !_Objects.Contains(g))
            {
                _Objects.Add(g);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Contains(GameObject g)
        {
            return _Objects.Contains(g);
        }

        //Adds a Unity GameObject to a list, takes an extra string for concurrent work loads
        public bool AddObject(GameObject g, string tag)
        {
            if (ObjectIsEigable(g, tag) && !_Objects.Contains(g))
            {
                _Objects.Add(g);
                return true;
            }
            else
            {
                return false;
            }
        }

        //Returns the size a group of objects
        public int SizeOfGroup()
        {
            if (_Objects == null)
            {
                return 0;
            }
            return _Objects.Count;
        }

        //Allows you to return the first Object in the groups of objects
        public GameObject GrabObject()
        {
            if(_Objects != null)
            {
                if (_Objects.Count > 0)
                    return _Objects[0];
                else return null;
            }
            else
            {
                return null;
            }
        }
        //Allows you to return a specified Object in the groups of objects
        public GameObject GrabObject(int Pos)
        {
            if (_Objects != null)
            {
                if (_Objects.Count > Pos)
                    return _Objects[Pos];
                else return null;
            }
            else
            {
                return null;
            }
        }
        //Finds and removes a specified object
        public void TakeOut(GameObject g)
        {
            if (_Objects.Contains(g))
            {
                for (int i = 0; i < _Objects.Count; i++)
                {
                    if (_Objects[i] == g)
                    {
                        _Objects.RemoveAt(i);
                    }
                }
            }
        }
        //Finds and removes a specified position
        private void TakeOut(int Pos)
        {
            _Objects.RemoveAt(Pos);
        }

        public void CleanFlag()
        {
            _AddedSucessfully = false;
        }
    }
    private void NullCheckInventory()
    {
        if (_Inventory == null)
        {
            _Inventory = new ConcurrentDictionary<int, InventoryItem>();
        }
    }

    //adds an object to an already existing pool of objects, if no pool exists then it creates a new pool
    public void AddObjectToInvent(GameObject go)
    {
        string Tag = go.tag;
        if (_Inventory.Count == 0)
        {
            CreateInventorySpace(go);
        }
        foreach (var item in _Inventory)
        {
            bool result = item.Value.AddObject(go, Tag);
            if (!result && !item.Value._AddedSucessfully)
            {
                CreateInventorySpace(go, Tag);
            }
        }
        foreach (var item in _Inventory)
        {
            item.Value.CleanFlag();
        }
    }

    //Uses multiple threads to add an object to an already existing pool of objects, if no pool exists then it creates a new pool
    public void AysncAddObjectToInvent(GameObject go)
    {
        string Tag = go.tag;
        if (_Inventory.Count == 0)
        {
            CreateInventorySpace(go);
        }
        Parallel.ForEach(_Inventory, (item) => {
            bool result = item.Value.AddObject(go, Tag);
            if (!result && !item.Value._AddedSucessfully)
            {
                CreateInventorySpace(go, Tag);
            }
        });
        Parallel.ForEach(_Inventory, (item) => { item.Value.CleanFlag(); });
    }

    //Creates a spot in the inventory for an object and adds it
    private void CreateInventorySpace(GameObject go)
    {
        InventoryItem II = new InventoryItem(go);
        _Inventory.TryAdd(_Inventory.Count + 1, II);
    }
    //Creates a spot in the inventory for an object and adds it
    private void CreateInventorySpace(GameObject go, string Tag)
    {
        InventoryItem II = new InventoryItem(go, Tag);
        _Inventory.TryAdd(_Inventory.Count + 1, II);
    }

    //Returns a specfied object from the inventory
    public GameObject GrabObjectFromInvent(int Place)
    {
        InventoryItem _InventoryItem;
        _Inventory.TryGetValue(Place, out _InventoryItem);
        return _InventoryItem.GrabObject();
    }
    //Returns a specfied object from the inventory
    public GameObject GrabObjectFromInvent(int Place, int ItemPlace)
    {
        InventoryItem _InventoryItem;
        _Inventory.TryGetValue(Place, out _InventoryItem);
        return _InventoryItem.GrabObject(ItemPlace);
    }

    //remove an object from the inventory
    public void RemoveObject(GameObject go)
    {
        foreach (var item in _Inventory)
        {
            item.Value.TakeOut(go);
        }
        for (int i = 0; i < _Inventory.Count; i++)
        {
            InventoryItem _InventoryItem;
            _Inventory.TryGetValue(i, out _InventoryItem);
            if (_InventoryItem.SizeOfGroup() != 0)
            {
                _Inventory.TryRemove(i, out _InventoryItem);
            }
        }
    }

    public bool Contains(GameObject g)
    {
        foreach (var item in _Inventory.Values)
        {
            if (item.Contains(g))
            {
                return true;
            }
        }
        return false;
    }

    //Uses multiple threads to remove an object
    public void AysncRemoveObject(GameObject go)
    {
        Parallel.ForEach(_Inventory, (item) => { item.Value.TakeOut(go); });

        Parallel.For(0, _Inventory.Count, index => {
            InventoryItem _InventoryItem;
            _Inventory.TryGetValue(index, out _InventoryItem);
            if (_InventoryItem.SizeOfGroup() != 0)
            {
                _Inventory.TryRemove(index, out _InventoryItem);
            };
        });
    }
}
