using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    [SerializeField]
    private Text m_PickUpText;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = transform.parent.parent.gameObject;
        if (!m_Player)
        {
            Debug.LogError("m_PickUpText is null om PlayerInteraction.cs");
        }
        else
        {
            if (m_Player.gameObject.name != "Player")
            {
                Debug.LogError("m_PickUpText is null om PlayerInteraction.cs");
            }
        }

        if (!m_PickUpText)
        {
            Debug.LogError("m_PickUpText is null om PlayerInteraction.cs");
        }
        m_PickUpText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Non-Interactable")
        {
            return;
        }
        m_PickUpText.enabled = true;
        m_PickUpText.text = "Pick Up " + other.gameObject.name;
    }

    void OnTriggerExit()
    {
        m_PickUpText.enabled = false;
    }

    //This will be called when an object will pass into the objects collider. see box collider
    void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(other.tag == "Non-Interactable" || other.tag == "Player")
            {
                return;
            }

            switch (other.name)
            {
                default:
                    break;
                case "Cube":
                    {
                        m_Player.GetComponent<Player>().AddObjectToInvent(other.gameObject);
                        other.gameObject.SetActive(false);
                        break;
                    }
            }
        }

    }
}
