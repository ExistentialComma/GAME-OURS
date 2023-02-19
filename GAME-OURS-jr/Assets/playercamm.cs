using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercamm : MonoBehaviour
{
    public float sensx;
    public float sensy;
    public Transform orientation;
    public float xRotation;
    public float yRotation;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    private void Update()
    {
        float mousex = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensx;
        float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensy;
        yRotation += mousex;
        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
    }
}
