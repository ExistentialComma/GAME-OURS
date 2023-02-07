using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    private float moveSpeed;
    public float walkspeed;
    public float sprintspeed;
    public float grounddrag;
    [Header("Jumping")]
    public float jumpf;
    public float jumpc;
    public float air;
    bool readytojump = true;
    [Header("Crouching")]
    public float crouchspeed;
    public float crouchyscale;
    private float startyscale;
    [Header("Cheite")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatitis;
    bool belle;
    public Transform orientation;
    float horizont;
    float vertical;
    Vector3 movementDirection;
    Rigidbody rb;
    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startyscale = transform.localScale.y;

            

    }
    private void Update()
    {
        belle = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatitis);
        Inputs();
        SpeedControl();
        StateHandler();
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
        if(Input.GetKey(jumpKey) && belle && readytojump)
        {
            
            jump();
            
            Invoke(nameof(Resetjump), jumpc);
            readytojump = false;
            
        }
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchyscale, transform.localScale.z);
            rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);

        }
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startyscale, transform.localScale.z);
        }
    }
    private void StateHandler()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchspeed;
        }
        if(belle && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintspeed;
        }
        else if (belle)
        {
            state = MovementState.walking;
            moveSpeed = walkspeed;
        }
        else
        {
            state = MovementState.air;
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
