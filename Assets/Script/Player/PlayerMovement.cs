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

    
    //step

    private AudioSource audio;
    [SerializeField] private AudioClip rockFootstep;
    [SerializeField] private AudioClip grassFootstep;

    [SerializeField] private float walkTimerSound = 0.02f;
    [SerializeField] private float runTimerSound = 0.01f;
    private float stepTimer;


    void Start()
    {
        Normalspeed2 = Normalspeed;
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        sprintBar = GetComponent<PlayerSprint>();
        audio = GetComponent<AudioSource>();

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
             manageFootStepSounds();
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

    private void manageFootStepSounds()
    {
        if (!IsGrounded) return;

        float currentTimer = IsRunningFast ? runTimerSound : walkTimerSound;

        stepTimer -= Time.deltaTime;
        
        if(stepTimer <= 0 && moveDirection != Vector3.zero)
        {
            PlayFootStepSound();
            stepTimer = currentTimer;
        }
    }

    private void PlayFootStepSound()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f)) return;

        //terrain
        Terrain terrain = hit.collider.GetComponent<Terrain>();
        if (terrain == null) return;

        TerrainData data = terrain.terrainData;

        //convert world position to terrain local position

        Vector3 terrainPosition = hit.point - terrain.transform.position;

        //convert to alpha map coordinates
        int mapx = Mathf.FloorToInt((terrainPosition.x / data.size.x) * data.alphamapWidth);
        int mapz = Mathf.FloorToInt((terrainPosition.z / data.size.z) * data.alphamapHeight);

        //get texture blend at the position
        float[,,] splatmap = data.GetAlphamaps(mapx, mapz, 1, 1);

        int textureIndex = 0;
        float strongest = 0f;

        //find the strongest texture at the position
        for (int i = 0; i < splatmap.GetLength(2); i++)
        {
            if (splatmap[0, 0, i] > strongest)
            {
                strongest = splatmap[0, 0, i];
                textureIndex = i;
            }
        }

        //assign sound based on texture index
        AudioClip clip = null;

        if (textureIndex == 0)
        {
            clip = rockFootstep;
        }
        else if (textureIndex == 1)
        {
            clip = grassFootstep;
        }

        if (clip != null)
        {
            audio.PlayOneShot(clip);
        }

    }
}
