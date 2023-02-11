using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cameraPosition;
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = cameraPosition.position;
    }
}
