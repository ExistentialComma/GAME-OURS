using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEntry : MonoBehaviour
{
    public GameObject Player;
    public GameObject Car;
    public Transform wheelFL;
    public Transform wheelFR;
    public bool inCar;
    public CarController carController;
    public Camera mainCamera;
    public Camera driveCamera;
    
    

    private void Start()
    {
        inCar = false;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && inCar == true)
        {
            inCar = false;
            Vector3 exitPosition = Car.transform.position + new Vector3(0, 5, 0);
            Player.transform.position = exitPosition;
            
            mainCamera.enabled = !mainCamera.enabled;
            driveCamera.enabled = !driveCamera.enabled;

            Player.gameObject.SetActive(true);
            carController.enabled = !carController.enabled;
        }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && inCar == false)
        {
            carController.enabled = !carController.enabled;
            carController.FixedUpdate();
            inCar = true;
            Player.gameObject.SetActive(false);
            mainCamera.enabled = !mainCamera.enabled;
            driveCamera.enabled = !driveCamera.enabled;
        }
        

    }
    
}
