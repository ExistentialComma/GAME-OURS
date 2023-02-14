using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelCon : MonoBehaviour
{
/*    //add a rigid body and box collider to the car
    //in public class  of CarControler script

    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider rearRight;
    [SerializeField] WheelCollider rearLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform rearRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform rearLeftTransform;
    //drag each wheel wesh to those transform things

    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    private void FixedUpdate()
    {
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakingForce;
        }
        else 
        {
            currentBreakForce = 0f;
        }
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        frontRight.breakTorque = currentBreakForce;
        rearRight.breakTorque = currentBreakForce;
        rearLeft.breakTorque = currentBreakForce;
        frontLeft.breakTorque = currentBreakForce;

        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;


        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(rearLeft, rearLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(rearRight, rearRightTransform);

       

        //now for gear change



    }*/
   
    

}
