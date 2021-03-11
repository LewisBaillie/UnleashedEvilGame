using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField]
    private GameObject _Player;

    private bool NearObject;

    void Start()
    {
        _Player = transform.parent.gameObject;
        if (!_Player)
        {
            Debug.LogError("m_PickUpText is null om PlayerInteraction.cs");
        }
        Physics.queriesHitTriggers = false;
    }

    void Update()
    {
        bool[] raysResults = new bool[360];
        RaycastHit[] raycastHits = new RaycastHit[360];

        /*
        for (int i = 0; i < 360; i++)
        {
            raycastHits[i] = new RaycastHit();
            Ray r = new Ray(transform.position, Quaternion.Euler(-15f, i, 0f).eulerAngles);
            raysResults[i] = Physics.Raycast(r,out raycastHits[i], 10f);
            ShowRay(r);
            ShowRay(r, raycastHits[i]);
        }
        foreach (bool Result in raysResults)
        {
            if (Result)
            { 
                NearObject = true;
                break;
            }
        }

        if (NearObject)
        {
            _Player.GetComponent<Player>().HeadCollison();
        }

        */
        ///*
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit raycastHit = new RaycastHit();
        bool Forward = Physics.Raycast(transform.position, Quaternion.Euler(-30f,0f,0f).eulerAngles, out raycastHit, 2f);
        raycastHits[0] = raycastHit;
        bool Backward = Physics.Raycast(transform.position,  Vector3.back, out raycastHit, 2f);
        raycastHits[1] = raycastHit;
        bool Right = Physics.Raycast(transform.position, Vector3.right, out raycastHit, 2f);
        raycastHits[2] = raycastHit;
        bool Left = Physics.Raycast(transform.position, Vector3.left, out raycastHit, 2f, 0);
        raycastHits[3] = raycastHit;

        raysResults[0] = Forward;
        raysResults[1] = Backward;
        raysResults[2] = Right;
        raysResults[3] = Left;



        //*/


        if (NearObject)
        {
           // _Player.GetComponent<Player>().HeadCollison();
        }
    }

    void ShowRay(Ray r)
    {
        Debug.DrawLine(r.origin, r.direction, Color.green);
    }

    void ShowRay(Ray r, RaycastHit rh)
    {
        Debug.DrawLine(r.origin, rh.point);
    }
}
