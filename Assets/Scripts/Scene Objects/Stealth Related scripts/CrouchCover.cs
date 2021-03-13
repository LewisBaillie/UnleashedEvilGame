using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchCover : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Parent;
    [SerializeField]
    private bool m_AutomaticSetUp;


    [SerializeField]
    private List<GameObject> m_Accessories;

    // Start is called before the first frame update
    void Start()
    {
        m_Parent = gameObject.transform.parent.gameObject;
        if (m_Parent == null)
        {
            Debug.LogWarning(gameObject.name + " will be destoryed as it couldn't find a parent.");
            Destroy(this);
        }
        if(m_AutomaticSetUp)
        {
            gameObject.GetComponent<BoxCollider>().size += new Vector3( 2 / m_Parent.transform.localScale.x, 2 / m_Parent.transform.localScale.y, 2 / m_Parent.transform.localScale.z) / 2;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().HeadCollison(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Player>().IsStanding())
            {
                foreach (GameObject obj in m_Accessories)
                {
                    if (obj.GetComponent<BoxCollider>())
                    {
                        obj.GetComponent<BoxCollider>().enabled = true;
                    }
                    if (obj.GetComponent<SphereCollider>())
                    {
                        obj.GetComponent<SphereCollider>().enabled = true;
                    }
                }
            }
            else
            {
                foreach (GameObject obj in m_Accessories)
                {
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
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().HeadCollison(false);
        }
    }
}
