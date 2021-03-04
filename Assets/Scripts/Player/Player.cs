using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;



public class Player : MonoBehaviour
{

    private struct PlayerItem
    {
        private string _Tag;
        private List<GameObject> _Objects;
        public PlayerItem(GameObject go, string Tag)
        {
            _Objects = new List<GameObject>();
            _Tag = Tag;
            _Objects.Add(go);
        }
        public PlayerItem(GameObject go)
        {
            _Objects = new List<GameObject>();
            _Tag = go.tag;
            _Objects.Add(go);
        }

        private bool ObjectIsEigable(GameObject g)
        {
            if (g.tag == _Tag && !_Objects.Contains(g))
            {
                return true;
            }
            else return false;
        }
        private bool ObjectIsEigable(GameObject g, string tag)
        {
            if (tag == _Tag && !_Objects.Contains(g))
            {
                return true;
            }
            else return false;
        }

        //Adds a Unity GameObject to a list
        public bool AddObject(GameObject g)
        {
            if (ObjectIsEigable(g))
            {
                _Objects.Add(g);
                return true;
            }
            else
            {
                return false;
            }
        }

        //Adds a Unity GameObject to a list, takes an extra string for concurrent work loads
        public bool AddObject(GameObject g, string tag)
        {
            if (ObjectIsEigable(g, tag))
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
            return _Objects.Count;
        }

        //Allows you to return the first Object in the groups of objects
        public GameObject GrabObject()
        {
            if (_Objects.Count > 0)
                return _Objects[0];
            else return null;
        }
        //Allows you to return a specified Object in the groups of objects
        public GameObject GrabObject(int Pos)
        {
            if (_Objects.Count > Pos)
            {
                return _Objects[Pos];
            }
            else return null;
        }
        //Finds and removes a specified object
        public void TakeOut(GameObject g)
        {
            if (_Objects.Contains(g))
            {
                for (int i = 0; i < _Objects.Count; i++)
                {
                    if(_Objects[i] == g)
                    {
                        _Objects.RemoveAt(i);
                    }
                }
            }
        }
        //Finds and removes a specified position
        public void TakeOut(int Pos)
        {
            _Objects.RemoveAt(Pos);
        }
    }

    [SerializeField]
    private float _Speed;
    [SerializeField]
    ConcurrentDictionary<int,PlayerItem> _Inventory;
    [SerializeField]
    private bool _IsGravActive;

    private CharacterController m_Controller;
    private float m_Gravity = 9.81f;

    // Start is called before the first frame update
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        NullCheckCharController();
        NullCheckInventory();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Move(ConvertToWorldSpace(ApplyGravity(direction * _Speed))); ;
    }

    private Vector3 ApplyGravity(Vector3 direction)
    {
        if(!_IsGravActive)
        {
            return direction;
        }
        direction.y -= m_Gravity;
        return direction;
    }

    private Vector3 ConvertToWorldSpace(Vector3 direction) //This Converts the Local Space direction of the Player into World Space
    {
        direction = transform.TransformDirection(direction);
        return direction;
    }

    private void Move(Vector3 newPosition)//Calls the Move Function on the Character Controller
    {
        m_Controller.Move(newPosition * Time.deltaTime);
    }

    private void NullCheckCharController()
    {
        if (m_Controller == null)
        {
            Debug.LogError("Character Controller on the Player was null");
        }
    }
    private void NullCheckInventory()
    {
        if(_Inventory == null)
        {
            _Inventory = new ConcurrentDictionary<int, PlayerItem>();
        }
    }

    public void AddObjectToInvent(GameObject go)
    {
        string Tag = go.tag;
        if (_Inventory.Count == 0)
        {
            CreateInventorySpace(go);
        }
        Parallel.ForEach(_Inventory, (item) => { 
            bool result = item.Value.AddObject(go, Tag);
            if (!result)
            {
                CreateInventorySpace(go, Tag);
            } 
        });
    }

    private void CreateInventorySpace(GameObject go)
    {
        PlayerItem PI = new PlayerItem(go);
        _Inventory.TryAdd(_Inventory.Count + 1, PI);
    }

    private void CreateInventorySpace(GameObject go, string Tag)
    {
        PlayerItem PI = new PlayerItem(go, Tag);
        _Inventory.TryAdd(_Inventory.Count + 1, PI);
    }

    public GameObject GrabObjectFromInvent(int Place)
    {
        PlayerItem _PlayerItem;
        _Inventory.TryGetValue(Place, out _PlayerItem);
        return _PlayerItem.GrabObject();
    }

    public void RemoveObject(GameObject go)
    {
        Parallel.ForEach(_Inventory, (item) => { item.Value.TakeOut(go); });

        Parallel.For(0, _Inventory.Count, index => {
            PlayerItem _PlayerItem;
            _Inventory.TryGetValue(index, out _PlayerItem);
            if (_PlayerItem.SizeOfGroup() == 0)
            {
                _Inventory.TryRemove(index, out _PlayerItem);
            };
        });
       

    }
}
