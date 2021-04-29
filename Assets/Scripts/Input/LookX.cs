using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{

    enum MouseStateX
    {
        Normal,
        Inverted
    }

    [SerializeField]
    private float m_MouseSensitivityX = 1f;
    [SerializeField]
    private MouseStateX m_MouseState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_MouseState)
        {
            case MouseStateX.Normal:
                {
                    RetrieveMouseInputX();
                    break;
                }
            case MouseStateX.Inverted:
                {
                    RetrieveMouseInputXInverted();
                    break;
                }
        }
    }

    private void RetrieveMouseInputX()
    {
        float _mouseX = Input.GetAxis("Mouse X");
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + ApplySensitivity(_mouseX), transform.localEulerAngles.z);
    }
    private void RetrieveMouseInputXInverted()
    {
        float _mouseX = Input.GetAxis("Mouse X");
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - ApplySensitivity(_mouseX), transform.localEulerAngles.z);
    }

    private float ApplySensitivity(float MouseInput)
    {
        MouseInput *= m_MouseSensitivityX;
        return MouseInput;
    }
}
