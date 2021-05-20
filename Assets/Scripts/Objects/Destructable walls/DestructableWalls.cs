using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DestructableWalls : InteractableObj
{
    public void ActivateDestruction(Vector3 AIForce)
    {
        foreach (Rigidbody body in GetComponentsInChildren<Rigidbody>())
        {
            body.gameObject.transform.parent = null;
            body.AddForce(AIForce, ForceMode.VelocityChange);
        }
        Destroy(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) ;
        {
            ActivateDestruction(Vector3.forward * 3);
        }
    }
}
