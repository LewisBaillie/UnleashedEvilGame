using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdentifier : MonoBehaviour
{
    [SerializeField]
    private int ID;

    public int returnID()
    {
        return ID;
    }
}
