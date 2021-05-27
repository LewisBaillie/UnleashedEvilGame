using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DestructableWalls : InteractableObj
{
    [SerializeField]
    private int _ForceMagnification;
    [SerializeField]
    private GameObject _Rubble;
    [SerializeField]
    private Vector3 _OriginPoint;
    [SerializeField]
    private int _RubbleAmount;
    [SerializeField]
    private int _RubbleTimeLimit;

    public void ActivateDestruction(Vector3 AIForce)
    {
        System.Random r = new System.Random();
        GameObject Parent = new GameObject(this.name + " Rubble Container");
        Parent.transform.position = this.transform.position;
        for (int i = 0; i < _RubbleAmount; i++)
        {
            GameObject g = Instantiate(_Rubble, _OriginPoint, Quaternion.identity);
            g.transform.parent = Parent.transform;
            g.GetComponent<Rigidbody>().AddForce(AIForce * NextFloat(r), ForceMode.VelocityChange);
            Destroy(g,_RubbleTimeLimit);
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

    void OnTriggerEnter(Collider other)
    {

    }

    //https://stackoverflow.com/questions/3365337/best-way-to-generate-a-random-float-in-c-sharp
    private float NextFloat(System.Random random)
    {
        double mantissa = (random.NextDouble() * 2.0) - 1.0;
        double exponent = Math.Pow(2.0, random.Next(0, _ForceMagnification));
        return (float)(mantissa * exponent);
    }
}
