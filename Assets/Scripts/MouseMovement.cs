using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
   
  
    public Transform orientation;
    public Transform gun;
    
    private float mouseX;
    private float mouseY;

    private float xRotation;
    private float yRotation;

    public float sensitivity = 1f;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        GetInput(); 
        RotatePlayer();
    }

    public void GetInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.fixedDeltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.fixedDeltaTime;

       
    }

    
    public void RotatePlayer()//and gun :|
    {
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseX;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        gun.rotation = Quaternion.Euler(xRotation,yRotation,0);
    
    }
}
