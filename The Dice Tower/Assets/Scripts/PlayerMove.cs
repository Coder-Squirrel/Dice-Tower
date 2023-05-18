using System;
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

    public GameObject swordHitbox,sword,shield;

    CharacterController characterController;

    public Animator anim;

    public bool isMovementPressed,weaponsOn;
    public bool isAttacking = false;
    public bool isBlocking = false;

    public event EventHandler OnInteract;

    public NPCHandler barril;

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
        barril.OnObjectInteract += GiveWeapons;
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
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            //Dispara o Evento a ser Escutado por objetos Interag�veis
            OnInteract?.Invoke(this, EventArgs.Empty);
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

    private void GiveWeapons(object sender, EventArgs args)
    {
        weaponsOn = true;
        sword.SetActive(true);
        shield.SetActive(true);
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

    //Lida com a Gravidade
    void handleGravity()
    {
        //Muda a gravidade do jogador para ----
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
        }
        //Volta a gravidade para o normal
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
            //inicia a anima��o de block
            anim.SetBool("isBlocking", true);
            StartCoroutine(InitaliseBlock());
        }
    }

    void cancelBlock()
    {
        if (isBlocking)
        {
            //desliga a anima��o de blocking
            anim.SetBool("isBlocking", false);
            isBlocking = false;
        }
    }


    void Attack()
    {
        if (!isAttacking)
        {
            swordHitbox.SetActive(true);
            StartCoroutine(EndAttack());
            //inicia a anima��o de ataque
            anim.SetTrigger("isAttacking");
            StartCoroutine(InitialiseAttack());
        }
    }

    void CancelAttack()
    {
        if (isAttacking)
        {
            
            //cancela o ataque depois de 0,5 segundos
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
        //ataca depois de x
        yield return new WaitForSeconds(0.1f);
        isAttacking = true;
    }

    IEnumerator EndAttack()
    {
        //ataca depois de x
        yield return new WaitForSeconds(0.5f);
        swordHitbox.SetActive(false);
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
