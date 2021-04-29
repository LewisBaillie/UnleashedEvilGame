using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides basic functions to do with movement and is the base for multiple objects that can move
/// </summary>
public class MoveableObj : Object
{
    [Header("Movement Settings")]
    [Tooltip("Controls factors to do with movement")]
    [SerializeField]
    private float _Speed;
    [SerializeField]
    private float _JumpStrength;
    [SerializeField]
    private float _JumpSpeed;
    [Header("Crouch Settings")]
    [Tooltip("Controls factors to do with crouching")]
    [SerializeField]
    private float _StandHeight;
    [SerializeField]
    private float _CrouchHeight;
    [SerializeField]
    private float _CrouchAnimSpeed;
    [SerializeField]
    private bool _IsCrouched;
    [SerializeField]
    private bool _CanStand;
    [Header("Physics Settings")]
    [Tooltip("Controls factors to do with physics")]
    [SerializeField]
    private bool _IsGravActive;
    [SerializeField]
    private float _Gravity = 9.81f;

    private float _Time = 0;
    private CharacterController _Controller;

    virtual public void SetUpComponent()
    {
        _objType = ObjectType.MoveableObj;
        _type = "";
    }


    
    //Applies Gravity based on the set strength
    protected Vector3 ApplyGravity(Vector3 direction)
    {
        if (!_IsGravActive)
        {
            return direction;
        }
        direction.y -= _Gravity;
        return direction;
    }

    //Calculates the height of a jump. extremely basic, because we may not want a jump func
    private Vector3 CalculateJumpHeight(Vector3 direction)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _Time += Time.deltaTime / _JumpSpeed;
            direction.y += Mathf.Lerp(direction.y, _JumpStrength, _Time);
            return direction;
        }
        return direction;
    }


    //Calls the Move Function on the Character Controller
    private void Move(Vector3 newPosition)
    {
        if(_Controller == null)
            _Controller = GetComponent<CharacterController>();
        _Controller.Move(newPosition * Time.deltaTime);
    }

    //Calculates movement, values are calculates through using the keyboard input
    protected void CalculateMovement()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Move(ConvertToWorldSpace(ApplyGravity(CalculateJumpHeight(dir) * _Speed))); ;
    }
    //Calculates movement, values are passed through the function
    protected void CalculateMovement(Vector3 direction)
    {
        Move(ConvertToWorldSpace(ApplyGravity(direction * _Speed))); ;
    }

    //Calculates the final height. It is caluclated through the _CrouchHeight & _StandHeight values 
    protected void CalculateHeight()
    {
        if (_IsCrouched)
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, _CrouchHeight, _Time), transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, _StandHeight, _Time), transform.localScale.z);
        }
    }

    //Allows for the height to be manipulated
    protected void HieghtMaipulation()
    {
        _Time += Time.deltaTime / _CrouchAnimSpeed;
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_IsCrouched && _CanStand)
            {
                _IsCrouched = false;
                CalculateHeight();
                _Time = 0;
            }
            else
            {
                _IsCrouched = true;
                CalculateHeight();
                _Time = 0;
            }
        }
    }

    //This converts the local space direction of the object into World Space
    protected Vector3 ConvertToWorldSpace(Vector3 direction)
    {
        return transform.TransformDirection(direction);
    }

    protected float ReturnSpeed()
    {
        return _Speed;
    }

    //Checks if the object is standing
    public bool IsStanding()
    {
        if (_IsCrouched)
        {
            return false;
        }
        return true;
    }

    //Sets if the object can stand up after being crouched 
    public void RestrictStanding(bool val)
    {
        if (val)
        {
            _CanStand = false;
        }
        else
        {
            _CanStand = true;
        }
    }
}
