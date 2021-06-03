using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNoises : MonoBehaviour
{

    private float m_Radius = 35f;

    private GameObject[] m_Enemies;

    private float m_xDistance;
    private float m_zDistance;
    private float m_hypotenuseDistance;

    private float m_counter;
    private bool hasEnemy;
    private int enemyID;
    private int previousID;

    [SerializeField]
    private List<AudioClip> _monsterClips;

    [SerializeField]
    private List<AudioSource> _monsterSource;


    void Start()
    {
        m_Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        m_counter -= Time.deltaTime;

        if(m_counter <= 0f)
        {
            foreach (GameObject e in m_Enemies)
            {
                m_xDistance = (e.transform.position.x - this.transform.position.x);
                m_zDistance = (e.transform.position.z - this.transform.position.z);

                m_hypotenuseDistance = Mathf.Sqrt((m_xDistance * m_xDistance) + (m_zDistance * m_zDistance));

                if (m_hypotenuseDistance <= m_Radius)
                {
                    hasEnemy = true;
                    previousID = enemyID;
                    enemyID = e.GetComponent<MonsterID>().returnID();
                }
            }

            if(hasEnemy)
            {
                if(previousID != enemyID)
                {
                    switch (enemyID)
                    {
                        case 1:
                            switch(Random.Range(0,2))
                            {
                                case 0:
                                    _monsterSource[0].clip = _monsterClips[0];
                                    break;
                                case 1:
                                    _monsterSource[0].clip = _monsterClips[1];
                                    break;
                            }
                            _monsterSource[0].Play();
                            break;
                        case 2:
                            switch (Random.Range(0, 2))
                            {
                                case 0:
                                    _monsterSource[1].clip = _monsterClips[2];
                                    break;
                                case 1:
                                    _monsterSource[1].clip = _monsterClips[3];
                                    break;
                            }
                            _monsterSource[1].Play();
                            break;
                        case 3:
                            switch (Random.Range(0, 2))
                            {
                                case 0:
                                    _monsterSource[2].clip = _monsterClips[4];
                                    break;
                                case 1:
                                    _monsterSource[2].clip = _monsterClips[5];
                                    break;
                            }
                            _monsterSource[2].Play();
                            break;
                        case 4:
                            _monsterSource[3].Play();
                            break;
                    }
                    m_counter = 2f;
                }
                else
                {
                    m_counter = Random.Range(5, 10);
                }
                hasEnemy = false;
            }
            else
            {
                m_counter = 5f;
            }
        }
        
    }
}
