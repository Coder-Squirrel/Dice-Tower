using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]public  float moveSpeed = 10f;

    public float rotationSpeed = 10f;

    PlayerInputActions playerInput;

    Vector2 currentMovementInput;
    Vector3 currentMovement;

    CharacterController characterController;

    public Animator anim;

    public bool isMovementPressed;
    public bool isAttacking = false;
    public bool isBlocking = false;

    [SerializeField] public float animationFinishTime = 0.1f;
    
    void Awake()
    {
        playerInput = new PlayerInputActions();

        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;
        playerInput.Player.Move.performed += OnMovementInput;

        playerInput.Player.Attack.performed += context => Attack();
        playerInput.Player.Attack.canceled += context => CancelAttack();

        playerInput.Player.Block.performed += context => Block();
        playerInput.Player.Block.canceled += context => cancelBlock();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        handleGravity();
        handleAnimation();
        characterController.Move(currentMovement * moveSpeed * Time.deltaTime);
        if(currentMovement != Vector3.zero && isMovementPressed)
        {
            
            Quaternion toRotation = Quaternion.LookRotation(currentMovement, Vector3.up);


            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        characterController.transform.Rotate(Vector3.up * currentMovementInput.x * Time.deltaTime);
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleAnimation()
    {
        bool isWalking = anim.GetBool("isWalking");

        if(isMovementPressed && !isWalking)
        {
            anim.SetBool("isWalking", true);
        }
        else if(!isMovementPressed && isWalking)
        {
            anim.SetBool("isWalking", false);
        }
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
        }
    }

    void Block()
    {
        if (!isBlocking)
        {
            anim.SetBool("isBlocking", true);
            StartCoroutine(InitaliseBlock());
        }
    }

    void cancelBlock()
    {
        if (isBlocking)
        {
            anim.SetBool("isBlocking", false);
            isBlocking = false;
        }
    }


    void Attack()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("isAttacking");
            StartCoroutine(InitialiseAttack());
        }
    }

    void CancelAttack()
    {
        if (isAttacking)
        {
            StartCoroutine(CancelateAttack());
        }
    }

    IEnumerator CancelateAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    IEnumerator InitialiseAttack()
    {
        yield return new WaitForSeconds(0.1f);
        isAttacking = true;
    }

    IEnumerator InitaliseBlock()
    {
        yield return new WaitForSeconds(0.1f);
        isBlocking = true;
    }

    public void OnEnable()
    {
        playerInput.Player.Enable();
    }

    public void OnDisable()
    {
        playerInput.Player.Disable();
    }

}
