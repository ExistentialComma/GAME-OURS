using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseclick : MonoBehaviour
{
    [Header("AxisSensitivity")]
    public float sensitivity;
    public Transform playerel;
    private float xrot = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xrot -= mouseY;
        xrot = Mathf.Clamp(xrot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xrot, 0f, 0f);
        playerel.Rotate(Vector3.up * mouseX);
    }
}
