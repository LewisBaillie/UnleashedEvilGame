using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField]
    private Text m_PickUpText;

    // Start is called before the first frame update
    void Start()
    {
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
            switch (other.name)
            {
                default:
                    break;
                case "Cube":
                    {
                        other.gameObject.SetActive(false);
                        break;
                    }

            }
        }

    }
}
