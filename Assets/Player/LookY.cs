using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{

    enum MouseStateY
    {
        Normal,
        Inverted
    }

    [SerializeField]
    private float m_MouseSensitivityY = 1f;
    [SerializeField]
    private MouseStateY m_MouseState;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        switch(m_MouseState)
        {
            case MouseStateY.Normal:
            {
                    RetrieveMouseInputY();
                    break;
            }
            case MouseStateY.Inverted:
            {
                    RetrieveMouseInputYInverted();
                    break;
            }
        }

    }
    private void RetrieveMouseInputY()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - ApplySensitivity(mouseY), transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private void RetrieveMouseInputYInverted()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + ApplySensitivity(mouseY), transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private float ApplySensitivity(float MouseInput)
    {
        MouseInput *= m_MouseSensitivityY;
        return MouseInput;
    }
}
