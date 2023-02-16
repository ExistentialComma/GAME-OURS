using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeras : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    [Header("Movement")]
    public float speed;
    [Header("Velocity")]
    public float gravity;
    [Header("Ground Features")]
    public Transform groundcheck;
    public float grounddistance;
    [Header("Jumping ceasing")]
    public float jumpheight;
    public LayerMask groundmask;
    Vector3 velocity;
    bool belle;

    // Update is called once per frame
    void Update()
    {
        belle = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);
        if(belle && velocity.y < 0)
        {
            velocity.y = -2.5f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime   );
        if(Input.GetButtonDown("Jump") && belle)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
