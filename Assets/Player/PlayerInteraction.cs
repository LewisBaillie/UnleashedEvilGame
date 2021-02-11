using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ActualParent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
