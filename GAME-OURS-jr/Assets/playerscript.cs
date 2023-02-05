using UnityEngine;
public class playerscript : MonoBehaviour
{
    // Start is called before the first frame update
    public float pozX;
    public float pozY;
    public Transform ori;
    float xrot;
    float yrot;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FixedUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * pozX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * pozY;
        xrot -= mouseY;
        yrot += mouseX;
        xrot = Mathf.Clamp(xrot, -90f, 90f);
        transform.rotation = Quaternion.Euler(xrot, yrot, 0);
        ori.rotation = Quaternion.Euler(0, yrot, 0);
    }
}

