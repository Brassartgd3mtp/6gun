using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10;
    public float airMultiplier = 1;
    public float groundDrag = 5;
    [SerializeField] private float angleHorizontalLimit;
    [SerializeField] private float angleVerticalLimit;

    [Header("Jump")]
    public float jumpForce = 8;
    public float jumpCooldown = 0.25f;
    private bool readyToJump;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 2;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform orientation;
    private bool grounded;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    [HideInInspector] public Vector3 moveDirection;

    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] LayerMask wallLayerMask;

    private Rigidbody rb;

    public Animator animator;
    public Animator CamAnimator;


    //bool isTouchingWall;
    Transform wallColliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;


        readyToJump = true;
    }

    private void Update()
    {
        //Je fait un Raycast qui part de mon personnage et qui va en direction du sol pour d�tecter s'il est en contact avec le sol
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        GetInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (moveDirection.x != 0 && moveDirection.y <= 1.1)
        {
            animator.SetBool("IsMoving", true);
            CamAnimator.SetBool("IsMoving", true);
        }

        else
        {
            animator.SetBool("IsMoving", false);
            CamAnimator.SetBool("IsMoving", false);

        }


        if (moveDirection.y >=1.1 )
        {
            animator.SetBool("IsMoving", false);
            CamAnimator.SetBool("IsMoving", false);

            Debug.Log(CamAnimator);

        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Je vérifie si le personnage est en contact avec un mur dans la direction de déplacement
        bool isTouchingWall = Physics.BoxCast(transform.position, capsuleCollider.bounds.extents, moveDirection, out RaycastHit hitInfo, transform.rotation, 1.5f, wallLayerMask);

        // Si le personnage est au sol, ajoutez la force de déplacement
        if (grounded)
        {
            if (!isTouchingWall)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            else
            {
                Vector3 moveDirectionWithoutWall = Vector3.ProjectOnPlane(moveDirection, hitInfo.normal).normalized;
                rb.AddForce(moveDirectionWithoutWall * moveSpeed * 10f, ForceMode.Force);
            }
        }
        else
        {
            // Ajoutez la force en l'air en tenant compte de l'adhérence au mur
            if (!isTouchingWall)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
            else
            {
                Vector3 moveDirectionWithoutWall = Vector3.ProjectOnPlane(moveDirection, hitInfo.normal).normalized;
                rb.AddForce(moveDirectionWithoutWall * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
        }
    }

    private void SpeedControl()
    {
        //Limite la v�locit� max du joueur
        if (rb.velocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelHor = rb.velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelHor.x, rb.velocity.y, limitedVelHor.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        if(transform.position.y == 2) rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, capsuleCollider.bounds.extents * 2);
    }
}