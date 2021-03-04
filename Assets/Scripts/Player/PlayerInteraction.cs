using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    [SerializeField]
    private GameObject m_CurrentObject;
    private ConcurrentBag<GameObject> m_ColliderCache;
    [SerializeField]
    private Text m_PickUpText;
    //Toggles multithreading but causes a unity exception though it still does work
    [SerializeField]
    private bool m_UseMultithreading;

    // Start is called before the first frame update
    void Start()
    {
        m_ColliderCache = new ConcurrentBag<GameObject>();
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
        HandleInteractions();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Non-Interactable" || other.tag == "Player" || other.gameObject.GetComponent<SceneObjects>() == null || other.gameObject.GetComponent<SceneObjects>().m_IsEquipable != true)
        {
            return;
        }
        m_PickUpText.enabled = true;
        m_PickUpText.text = "Pick Up " + other.gameObject.name;
        m_ColliderCache.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        TryTakeOut(other.gameObject);
        HideText();
    }

    //Trys to remove the gameObject from the gameobject cache.
    private void TryTakeOut(GameObject gObject)
    {
        if (m_ColliderCache.IsEmpty)
        {
            return;
        }
        bool result = m_ColliderCache.TryTake(out gObject);
    }

    private void HideText()
    {
        if(m_ColliderCache.IsEmpty)
        {
            m_PickUpText.enabled = false; 
        }
        else
        {
            GameObject[] objects = m_ColliderCache.ToArray();
            m_PickUpText.text = "Pick Up " + objects[0].name;
        }
    }

    private void HandleInteractions()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(m_UseMultithreading)
            {
                Parallel.ForEach(m_ColliderCache, (item) => {
                    item.transform.parent = this.gameObject.transform;
                    m_Player.GetComponent<Player>().AddObjectToInvent(item);
                    item.SetActive(false);
                    m_ColliderCache.TryTake(out item);
                    HideText();
                });
            }
            else
            {
                GameObject[] goa = m_ColliderCache.ToArray();
                for (int i = 0; i < goa.Length; i++)
                {
                    goa[i].transform.parent = this.gameObject.transform;
                    m_Player.GetComponent<Player>().AddObjectToInvent(goa[i]);
                    goa[i].SetActive(false);
                    m_ColliderCache.TryTake(out goa[i]);
                    HideText();
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_CurrentObject != null)
                m_CurrentObject.SetActive(false);
            m_CurrentObject = m_Player.GetComponent<Player>().GrabObjectFromInvent(1);
            if(m_CurrentObject != null)
            {
                m_CurrentObject.GetComponent<SceneObjects>().m_IsEquipable = false;
                m_CurrentObject.GetComponent<SceneObjects>().m_IsEquipped = true;
                m_CurrentObject.transform.localPosition = new Vector3(0.208f, -0.158f, -0.258f);
                m_CurrentObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (m_CurrentObject != null)
                m_CurrentObject.SetActive(false);
            m_CurrentObject = m_Player.GetComponent<Player>().GrabObjectFromInvent(1);
            if (m_CurrentObject != null)
            {
                m_CurrentObject.GetComponent<SceneObjects>().m_IsEquipable = false;
                m_CurrentObject.GetComponent<SceneObjects>().m_IsEquipped = true;
                m_CurrentObject.transform.localPosition = new Vector3(0.208f, -0.158f, -0.258f);
                m_CurrentObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (m_CurrentObject != null)
                m_CurrentObject.SetActive(false);
            m_CurrentObject = m_Player.GetComponent<Player>().GrabObjectFromInvent(1);
            if (m_CurrentObject != null)
            {
                m_CurrentObject.GetComponent<SceneObjects>().m_IsEquipable = false;
                m_CurrentObject.GetComponent<SceneObjects>().m_IsEquipped = true;
                m_CurrentObject.transform.localPosition = new Vector3(0.208f, -0.158f, -0.258f);
                m_CurrentObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (m_CurrentObject != null)
                m_CurrentObject.SetActive(false);
            m_CurrentObject = m_Player.GetComponent<Player>().GrabObjectFromInvent(1);
            if (m_CurrentObject != null)
            {
                m_CurrentObject.GetComponent<SceneObjects>().m_IsEquipable = false;
                m_CurrentObject.GetComponent<SceneObjects>().m_IsEquipped = true;
                m_CurrentObject.transform.localPosition = new Vector3(0.208f, -0.158f, -0.258f);
                m_CurrentObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (m_CurrentObject != null)
                m_CurrentObject.SetActive(false);
            m_CurrentObject = m_Player.GetComponent<Player>().GrabObjectFromInvent(1);
            if (m_CurrentObject != null)
            {
                m_CurrentObject.GetComponent<SceneObjects>().m_IsEquipable = false;
                m_CurrentObject.GetComponent<SceneObjects>().m_IsEquipped = true;
                m_CurrentObject.transform.localPosition = new Vector3(0.208f, -0.158f, -0.258f);
                m_CurrentObject.SetActive(true);
            }
        }
    }
}
