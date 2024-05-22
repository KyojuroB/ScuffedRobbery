using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TextFacing : MonoBehaviour
{
    void Update()
    {
        // Get the main camera's transform
        Transform cameraTransform = Camera.main.transform;

        // Calculate the direction from the object to the camera
        Vector3 directionToCamera = cameraTransform.position - transform.position;

        // Zero out the y component to keep the text upright
        directionToCamera.y = 0;

        // Ensure the direction is not zero
        if (directionToCamera.sqrMagnitude > 0.001f)
        {
            // Make the object face the camera while keeping the text upright
            transform.rotation = Quaternion.LookRotation(directionToCamera, Vector3.up);
        }

        // Apply a 180-degree rotation around the y-axis to correct the orientation if needed
        transform.rotation *= Quaternion.Euler(0, 180, 0);
    }
}
