using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    public float Normalspeed = 4f;
    public float Normalspeed2;
    [SerializeField] private float fastRunMultiplier = 2f;
    private float gravity = -15f;
    private bool IsRunningFast;
    private bool IsGrounded;
    [SerializeField] private float jumpHeight = 2f;
    private Vector3 moveDirection;
    private Vector3 playerVelocity;
    private Animator anim;

    [SerializeField] private Transform cameraPosition;
    private PlayerSprint sprintBar;

    

    void Start()
    {
        Normalspeed2 = Normalspeed;
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        sprintBar = GetComponent<PlayerSprint>();

        if (cameraPosition == null)
        {
            cameraPosition = Camera.main.transform;
        }
    }
    public void SetCamera(Transform cam)
    {
        cameraPosition = cam;
    }

    private void OnMove(InputValue input)
    {
        Vector2 inputVector = input.Get<Vector2>();
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
    }
    private void OnRunFast(InputValue input)
    {
        IsRunningFast = input.isPressed;
    }
    private void OnJump(InputValue input)
    {
        if (IsGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);

            if(moveDirection == Vector3.zero)
            {
                anim.SetTrigger("Jump");
            }
            else
            {
                anim.SetTrigger("RunJump");
            }
                
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        

        IsGrounded = characterController.isGrounded;
        if(IsGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        float speed = Normalspeed;

        if (moveDirection != Vector3.zero)
        {
            if (IsRunningFast && sprintBar.canSprint)
            {
                speed *= fastRunMultiplier;
                sprintBar.UseSprint(Time.deltaTime);

                anim.SetBool("FastRun", true);
                anim.SetBool("Run", false);
            }
            else
            {
                sprintBar.regenerateSprint(Time.deltaTime);
            
                anim.SetBool("Run", true);
                anim.SetBool("FastRun", false);
            
            }

             Vector3 forward = cameraPosition.forward;
             Vector3 right = cameraPosition.right;
             forward.y = 0;
             right.y = 0;
             forward.Normalize();
             right.Normalize();

             Vector3 MoveWithCamera = (forward * moveDirection.z + right * moveDirection.x).normalized;
             Vector3 moving = MoveWithCamera * speed * Time.deltaTime;
             characterController.Move(moving);

            


             Quaternion rotate = Quaternion.LookRotation(MoveWithCamera, Vector3.up);
             transform.rotation = Quaternion.Lerp(transform.rotation, rotate, Time.deltaTime * 15f);
        }
        else
        {
            sprintBar.regenerateSprint(Time.deltaTime);
            anim.SetBool("Run", false);
            anim.SetBool("FastRun", false);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        }
}
