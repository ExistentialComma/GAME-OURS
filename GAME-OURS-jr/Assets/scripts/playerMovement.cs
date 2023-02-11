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
    public float slideSpeed;
    private float desireMoveSpeed;
    private float lastDesiredMoveSpeed;
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
    public KeyCode crouchKey = KeyCode.C;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatitis;
    bool belle;
    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopehit;
    private bool exitingslope;

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
        sliding,
        air
    }
    public bool sliding;
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
            rb.AddForce(Vector3.down * 1f, ForceMode.Impulse);

        }
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startyscale, transform.localScale.z);
        }
    }
    private void StateHandler()
    {
        if (sliding)
        {
            state = MovementState.sliding;
            if(onslope() && rb.velocity.y < 0.1f)
            {
                desireMoveSpeed = slideSpeed;
            }
            else
            {
                desireMoveSpeed = sprintspeed;
            }
        }
        else if (Input.GetKeyDown(crouchKey))
        {
            state = MovementState.crouching;
            desireMoveSpeed = crouchspeed;
        }
        else if(belle && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desireMoveSpeed = sprintspeed;
        }
        else if (belle)
        {
            state = MovementState.walking;
            desireMoveSpeed = walkspeed;    
        }
        else
        {
            state = MovementState.air;
        }
        if (Mathf.Abs(desireMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desireMoveSpeed;
        }
        lastDesiredMoveSpeed = desireMoveSpeed;

    }
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desireMoveSpeed - moveSpeed);
        float startValue = moveSpeed;
        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desireMoveSpeed, time / difference);
            time += Time.deltaTime;
            yield return null;
        }
        moveSpeed = desireMoveSpeed;
    }
    private void MovePlayer()
    {
        movementDirection = orientation.forward * vertical + orientation.right * horizont;
        if (onslope() && !exitingslope)
        {
            rb.AddForce(GetSlopeMoveDirection(movementDirection) * moveSpeed * 20f, ForceMode.Force);
            if(rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        else if (belle)
        {
            rb.AddForce(movementDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!belle)
        {
            rb.AddForce(movementDirection.normalized * moveSpeed * 10f * air, ForceMode.Force);
        }
        rb.useGravity = !onslope();
    }
    private void SpeedControl()
    {
        if (onslope() && !exitingslope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }
    private void jump()
    {
        exitingslope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpf, ForceMode.Impulse);
    }
    private void Resetjump()
    {
        readytojump = true;
        exitingslope = false;
    }
    public bool onslope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopehit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopehit.normal);
            return angle < maxSlopeAngle && angle != 0;

        }
        return false;
    }
    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopehit.normal).normalized;
    }
}
