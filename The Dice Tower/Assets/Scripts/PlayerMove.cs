using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    PlayerInputActions playerInput;

    Vector2 currentMovementInput;
    Vector3 currentMovement;

    CharacterController characterController;

    public Animator anim;

    public bool isMovementPressed;
    public bool isAttacking = false;
    public bool isBlocking = false;

    [SerializeField] private float animationFinishTime = 0.1f;



    void Awake()
    {
        playerInput = new PlayerInputActions();

        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;
        playerInput.Player.Move.performed += OnMovementInput;

        playerInput.Player.Attack.performed += context => Attack();

        playerInput.Player.Block.performed += context => Block();
        playerInput.Player.Block.canceled += context => cancelBlock();
    }

    void Start()
    {

    }

    void Update()
    {
        handleAnimation();
        characterController.Move(currentMovement * moveSpeed * Time.deltaTime);
        if(isAttacking)
        {
            isAttacking = false;
        }
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

    void OnEnable()
    {
        playerInput.Player.Enable();
    }

    void OnDisable()
    {
        playerInput.Player.Disable();
    }

}
