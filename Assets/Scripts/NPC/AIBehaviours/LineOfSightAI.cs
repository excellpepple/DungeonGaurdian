using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightAI : MonoBehaviour
{
    public float rotationSpeed;
    public float visionDistance;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,  rotationSpeed * Time.deltaTime, 0);
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo,  visionDistance))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
            
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * visionDistance, Color.green);
        }
    }
}
