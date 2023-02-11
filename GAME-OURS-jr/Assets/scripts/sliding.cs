using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliding : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("References")]
    public Transform orientation;
    public Transform playerobj;
    private Rigidbody rb;
    private playerMovement pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;
    public float slideYscale;
    private float startYscale;
    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizont;
    private float vertic;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<playerMovement>();
        startYscale = playerobj.localScale.y;
    }
    private void Update()
    {
        horizont = Input.GetAxisRaw("Horizontal");
        vertic = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(slideKey) && (horizont != 0 || vertic != 0))
        {
            StartSlide();
        }
        if (Input.GetKeyUp(slideKey) && pm.sliding)
        {
            StopSlide();
        }
    }
    private void FixedUpdate()
    {
        if (pm.sliding)
        {
            SlidingMovement();
        }
    }
    private void StartSlide()
    {
        pm.sliding = true;
        playerobj.localScale = new Vector3(playerobj.localScale.x, slideYscale, playerobj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        slideTimer = maxSlideTime;
    }
    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * vertic + orientation.right * horizont;
        if (!pm.onslope() || rb.velocity.y > -0.1f)
        {
            
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
            slideTimer -= Time.deltaTime;
        }
        else
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }
        if (slideTimer <= 0)
        {
            StopSlide();
        }
    }
    private void StopSlide()
    {
        pm.sliding = false;
        playerobj.localScale = new Vector3(playerobj.localScale.x, startYscale, playerobj.localScale.z);
    }
}
