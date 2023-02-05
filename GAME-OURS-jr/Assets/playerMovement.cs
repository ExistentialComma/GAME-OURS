using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    public float moveSpeed;
    public float grounddrag;
    public float jumpf;
    public float jumpc;
    public float air;
    bool readytojump;
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatitis;
    bool belle;
    public Transform orientation;
    float horizont;
    float vertical;
    Vector3 movementDirection;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
            

    }
    private void Update()
    {
        belle = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatitis);
        Inputs();
        SpeedControl();
        if (belle)
        {
            rb.drag = grounddrag;
        }
        else
        {
            rb.drag = 0;
        }
        
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void Inputs()
    {
        horizont = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(jumpKey) && belle)
        {
            readytojump = false;
            jump();
            Invoke(nameof(Resetjump), jumpc);
        }
    }
    private void MovePlayer()
    {
        movementDirection = orientation.forward * vertical + orientation.right * horizont;
        if (belle)
        {
            rb.AddForce(movementDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!belle)
        {
            rb.AddForce(movementDirection.normalized * moveSpeed * 10f * air, ForceMode.Force);
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        } 
    }
    private void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpf, ForceMode.Impulse);
    }
    private void Resetjump()
    {
        readytojump = true;
    }
}
