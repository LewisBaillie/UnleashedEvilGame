using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DestructableWalls : InteractableObj
{
    [SerializeField]
    public void ActivateDestruction(Vector3 AIForce)
    {
   
        Rigidbody[] r = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in GetComponentsInChildren<Rigidbody>())
        {
            body.useGravity = true;
            body.gameObject.transform.parent = null;
            body.AddForce(AIForce, ForceMode.VelocityChange);
            CapsuleCollider c = body.GetComponent<CapsuleCollider>();
            c.enabled = true;
        }
        Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ActivateDestruction(Vector3.left * 3);
        }
    }
}
