using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _Speed;
    [SerializeField]
    List<GameObject> _Inventory;
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
            _Inventory = new List<GameObject>();
        }
    }

    public void AddObjectToInvent(GameObject go)
    {
        foreach (GameObject item in _Inventory)
        {
            if (item.name == go.name && item.tag == go.tag)
            {
                return;
            }
        }
        if (go.GetComponent<SceneObjects>() != null)
        {
            _Inventory.Add(go);
            return;
        }
        Debug.Log(go.name + " doesn't have the class SceneObjects attached so it hasn't been added to the inventory");
    }
}
