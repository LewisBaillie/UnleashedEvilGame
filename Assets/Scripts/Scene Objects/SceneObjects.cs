using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneObjects : MonoBehaviour
{
    public bool m_IsEquipable = false;
    public bool m_IsEquipped { get; set; } = false;
    [SerializeField]
    private Image m_Icon;
    [SerializeField]
    private float m_Dampener;
    private Vector3 m_ActiveForce;
    private Vector3 m_Direction;

    void Start()
    {
        if(m_Dampener == 0.0f && m_IsEquipable)
        {
            Debug.LogWarning(gameObject.name + "has no damperning effect when thrown. If this is intentional, ignore this");
        }
        if (m_IsEquipable)
        {
            gameObject.AddComponent<CharacterController>();
            gameObject.AddComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (!m_IsEquipped)
        {
            if (m_ActiveForce.x > 0 || m_ActiveForce.y > 0 || m_ActiveForce.z > 0)
            {
                m_ActiveForce = m_ActiveForce * m_Dampener;
                gameObject.GetComponent<CharacterController>().Move(ConvertToWorldSpace(m_ActiveForce));
            }
        }
        else
        {
            m_ActiveForce = new Vector3(0,0,0);
        }
    }


    public bool HasIcon() { if (m_Icon) { return true; } return false; }

    public Image GiveIcon() { return m_Icon; }

    public void AddForce(Vector3 Force)
    {
        m_ActiveForce = Force;
    }

    private Vector3 ConvertToWorldSpace(Vector3 direction) //This Converts the Local Space direction of the Player into World Space
    {
        direction = transform.TransformDirection(direction);
        return direction;
    }
}