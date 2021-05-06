using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObj : MoveableObj
{
<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
    }
=======
    [Header("In Hand Settings")]
    [Tooltip("Controls factors to do with Objects in the hand")]
    [SerializeField]
    protected bool _InHand;
    [SerializeField]
    private Vector3 _RelativeHandPosition;
>>>>>>> Stashed changes

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void UpdatePosition()
    {
        if (transform.parent != null)
        {
            transform.position = transform.parent.position;
        }
    }

    public void SetRelativePos(Vector3 pos)
    {
        _RelativeHandPosition = pos;
    }
    public Vector3 GetRelativePos()
    {
        return _RelativeHandPosition;
    }
}
