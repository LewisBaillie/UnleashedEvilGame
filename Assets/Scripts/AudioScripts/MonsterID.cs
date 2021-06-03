using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterID : MonoBehaviour
{
    [SerializeField]
    private int monsterID;

    public int returnID()
    {
        return monsterID;
    }
}
