using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform target; 

    public Transform[] targets;

    public int currentTarget = 0;

    private float rotationSpeed = 5f;
    private float zoomSpeed = 5f;
    private float minZoomDistance = 5f;
    private float maxZoomDistance = 20f;
    private float verticalMovementSpeed = 1f;

    private void Update()
    {
        // Camera rotation
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.RotateAround(target.position, Vector3.up, mouseX);
        }

        // Camera zoom
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        float zoomAmount = scrollWheelInput * zoomSpeed;

        // Apply zoom
        Vector3 zoomDirection = (transform.position - target.position).normalized;
        float currentZoomDistance = Vector3.Distance(transform.position, target.position);
        float newZoomDistance = Mathf.Clamp(currentZoomDistance - zoomAmount, minZoomDistance, maxZoomDistance);
        Vector3 newCameraPosition = target.position + zoomDirection * newZoomDistance;

        transform.position = newCameraPosition;

        // Camera vertical movement
        if (Input.GetMouseButton(1))
        {
            float mouseY = Input.GetAxis("Mouse Y") * verticalMovementSpeed;
            Vector3 newPosition = transform.position + transform.up * mouseY;
            transform.position = newPosition;
        }
    }

        public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

        public void nextTarget()
    {
        if(currentTarget == 2){
            currentTarget = 0;
        }else{
            currentTarget++;
        }
        target = targets[currentTarget];
        Camera.main.transform.position = target.position + new Vector3(0f, -2f, -8f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
        Camera.main.transform.rotation = rotation;
    }

         public void previousTarget()
    {
        if(currentTarget == 0){
            currentTarget = 2;
        }else{
            currentTarget--;
        }
        target = targets[currentTarget];
         Camera.main.transform.position = target.position + new Vector3(0f, -2f, -8f);
         Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
         Camera.main.transform.rotation = rotation;
    }
}