using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNoise : MonoBehaviour
{
    private GameObject[] m_Enemies;

    private float m_xDistance;
    private float m_zDistance;
    private float m_hypotenuseDistance;
    private float m_Radius = 35;

    private float m_counter;

    private bool hasEnemy;

    private int enemyID;
    private int previousID;

    [SerializeField]
    private AudioSource quickSource;

    [SerializeField]
    private AudioSource bigSource;

    [SerializeField]
    private AudioSource crawlerSource;

    [SerializeField]
    private AudioSource insaneSource;

    [SerializeField]
    private AudioClip quickSound1;

    [SerializeField]
    private AudioClip quickSound2;

    [SerializeField]
    private AudioClip bigSound1;

    [SerializeField]
    private AudioClip bigSound2;

    [SerializeField]
    private AudioClip crawlerSound1;

    [SerializeField]
    private AudioClip crawlerSound2;

    [SerializeField]
    private AudioClip insaneSound;


    // Start is called before the first frame update
    void Start()
    {
        enemyID = 0;
        hasEnemy = false;
        m_counter = 5f;
        m_Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        m_counter -= Time.deltaTime;
        if(m_counter <= 0)
        {
            foreach (GameObject e in m_Enemies)
            {
                m_xDistance = (e.transform.position.x - this.transform.position.x);
                m_zDistance = (e.transform.position.z - this.transform.position.z);

                m_hypotenuseDistance = Mathf.Sqrt((m_xDistance * m_xDistance) + (m_zDistance * m_zDistance));

                if (m_hypotenuseDistance <= m_Radius)
                {
                    previousID = enemyID;
                    enemyID = e.GetComponent<MonsterIdentifier>().returnID();
                    hasEnemy = true;
                    break;
                }
            }
            if(hasEnemy == true)
            {
                if(previousID != enemyID)
                {
                    switch (enemyID)
                    {
                        case 1:
                            switch(Random.Range(0,2))
                            {
                                case 0:
                                    quickSource.clip = quickSound1;
                                    quickSource.Play();
                                    break;
                                case 1:
                                    quickSource.clip = quickSound2;
                                    quickSource.Play();
                                    break;
                            }
                            break;
                        case 2:
                            switch (Random.Range(0, 2))
                            {
                                case 0:
                                    bigSource.clip = bigSound1;
                                    bigSource.Play();
                                    break;
                                case 1:
                                    bigSource.clip = bigSound2;
                                    bigSource.Play();
                                    break;
                            }
                            break;
                        case 3:
                            switch (Random.Range(0, 2))
                            {
                                case 0:
                                    crawlerSource.clip = crawlerSound1;
                                    crawlerSource.Play();
                                    break;
                                case 1:
                                    crawlerSource.clip = crawlerSound2;
                                    crawlerSource.Play();
                                    break;
                            }
                            break;
                        case 4:
                            insaneSource.Play();
                            break;
                    }
                    m_counter = 2f;
                }
                m_counter = Random.Range(5,10);
            }
            else
            {
                m_counter = 5f;
            }
        }
    }
}