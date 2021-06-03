using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingObj : Obj
{
    private bool isThrown = false;

    void Start()
    {
        _objType = ObjectType.ThrowingObj;
        _type = "";
        GetComponent<BoxCollider>().isTrigger = true;
    }
    public void AddForce(Vector3 Force)
    {
        GetComponent<Rigidbody>().AddForce(transform.TransformVector(Force), ForceMode.Impulse);
    }

    public void SetIsThrown(bool t)
    {
        isThrown = t;
    }

    void OnTriggerEnter(Collider other)
    {
        if(isThrown && other.tag != "Player" && other.name != "Fog")
        {
            GameObject newObj = new GameObject("Audio");
            newObj.AddComponent<AudioSource>();
            newObj.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
            newObj.GetComponent<AudioSource>().Play();
            Destroy(newObj, newObj.GetComponent<AudioSource>().clip.length);
            GetComponent<MakeNoise>().MakeSound();
            Destroy(this.gameObject);
        }          
    }
}
