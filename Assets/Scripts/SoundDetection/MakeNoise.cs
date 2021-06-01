using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNoise : MonoBehaviour
{
    //20 is small
    [SerializeField]
    private float m_Radius;

    private GameObject[] m_Enemies;

    private float m_xDistance;
    private float m_zDistance;
    private float m_hypotenuseDistance;

    private void MakeSound()
    {
        m_Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in m_Enemies)
        {
            m_xDistance = (e.transform.position.x - this.transform.position.x);
            m_zDistance = (e.transform.position.z - this.transform.position.z);

            m_hypotenuseDistance = Mathf.Sqrt((m_xDistance * m_xDistance) + (m_zDistance * m_zDistance));

            if (m_hypotenuseDistance <= m_Radius)
            {
                e.GetComponent<Listener>().hearSound(this.transform);
            }

        }
    }

}
