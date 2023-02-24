using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    [Header("Move Variables")]
    private float speed;
    public float walkspeed;
    public float sprintspeed;

    public Transform orientation;
    float verticali;
    float horizonti;
    Vector3 movementDirection;
    Rigidbody rb;
    public float grounddrag;
    public float jumpf;
    public float jumpc;
    public float airm;
    bool readytojump = true;
    [Header("Ground")]
    public float height;
    public LayerMask whatitis;
    bool belle;
    [Header("Crouching")]
    public float crouchspeed;
    public float crouchyscale;
    public float startyscale;
    [Header("Chei")]
    public KeyCode jumpkey = KeyCode.Space;
    public KeyCode sprintkey = KeyCode.LeftShift;
    public KeyCode crouchkey = KeyCode.C;
    public MovementState state;
    public enum MovementState
    {
        walking,
        crouching,
        sprinting,
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
            belle = Physics.Raycast(transform.position, Vector3.down, height * 1f + 0.2f, whatitis);
            myinput();
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
    public void FixedUpdate()
    {
            Moving();
    }
    private void myinput()
    {
        horizonti = Input.GetAxisRaw("Horizontal");
        verticali = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(jumpkey) && readytojump && belle)
        {
            
            jump();
            Invoke(nameof(Resetjump), jumpc);
            readytojump = false;
        }
        
    }
    private void StateHandler()
    {
        if (belle && Input.GetKey(crouchkey))
        {
            state = MovementState.crouching;
            speed = crouchspeed;
            
        }
        if(belle && Input.GetKey(sprintkey))
        {
            state = MovementState.sprinting;
            speed = sprintspeed;
        }
        else if (belle)
        {
            state = MovementState.walking;
            speed = walkspeed;
        }
        else
        {
            state = MovementState.air;
        }
            
    }
    private void Moving()
    {
        movementDirection = orientation.forward * verticali + orientation.right * horizonti;
        if (belle)
        {
            rb.AddForce(movementDirection.normalized * speed * 1f, ForceMode.Impulse);
        }
        else if (!belle)
        {
            rb.AddForce(movementDirection.normalized * speed * 1f * airm, ForceMode.Force);
        }



    }
    private void SpeedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatvel.magnitude > speed)
        {
            Vector3 limitedvel = flatvel.normalized * speed;
            rb.velocity = new Vector3(limitedvel.x, rb.velocity.y, limitedvel.z);
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
