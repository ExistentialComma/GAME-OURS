using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
     //add a rigid body and box collider to the car
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
    public float brakingForce = 300f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBrakeForce = 0f;
    private float currentTurnAngle = 0f;

    private void FixedUpdate()
    {
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            currentBrakeForce = brakingForce;
        }
        else
        {
            currentBrakeForce = 0f;
        }
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBrakeForce;
        rearRight.brakeTorque = currentBrakeForce;
        rearLeft.brakeTorque = currentBrakeForce;
        frontLeft.brakeTorque = currentBrakeForce;

        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
    }





    }
