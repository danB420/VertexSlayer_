using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [Header("Movement Vars")]
    public float movementSpeed;
    private float horizontalInput;
    private float verticalInput;
   

     public Transform orientation;
    public Rigidbody rb;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    public KeyCode jumpKey;
    private bool canJump = true;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        CalculateInput();
      
             
      
    }
    private void FixedUpdate()
    {
        MovePlayer(); 
        HandleDrag(); 
        NormalizeSpeed();
    }

    private void CalculateInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
                      
        if (Input.GetKey(jumpKey) && canJump && grounded)
        {  
           
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
           
    }

    private void MovePlayer()
    {
      
        if(grounded)
        rb.AddForce(moveDirection.normalized * movementSpeed * 10f , ForceMode.Acceleration);
        else
            rb.AddForce(moveDirection.normalized * movementSpeed * 2f * airMultiplier, ForceMode.Acceleration);

    }


    private void CheckGrounded()
    {
        // pentru a verifica daca ne aflam in contact cu "podeaua", este folosit un Raycast
        // care traseaza o semidreapta verticala (raycast) ce trece prin jucator si daca se loveste
        // de un obiect  dedesubt de jucator inseamna ca ne aflam pe podea
        grounded = Physics.Raycast(orientation.position, Vector3.down, playerHeight * 0.5f+0.2f,whatIsGround);
       
    }

    private void HandleDrag()
    {
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
            rb.drag = 0;
    }

    private void NormalizeSpeed()
    {

        Vector3 flatVel = new Vector3(rb.velocity.x,0f,rb.velocity.z);  
        //daca viteza actuala e mai mare decat viteza supusa se calculeaza care ar fi viteza corecta si se face corectarea
        if(flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedSpeed = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedSpeed.x, rb.velocity.y, limitedSpeed.z);
        }
    }

    private void Jump()
    {
        
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
    }

    private void ResetJump()
    {
        canJump = true;
    }

    
}
