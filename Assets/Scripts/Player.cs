using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    //Visible to inspector
    [SerializeField] private Camera playerCamera;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;

    [Header("Player Attributes")]
    [SerializeField] private float walkSpeed = 7.5f;
    [SerializeField] private float runSpeed = 11.5f;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 70.0f;
    
    //Other classes but not inspector
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool isRunning = false;

    //Only this class
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //-------------------------------------------------------------
        //              Mouse aim and movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = 0.0f;
        float curSpeedY = 0.0f;

        //TODO i dont think this is the best place to put this
        if (Input.GetKey(KeyCode.W))
        {
            // Consider player moving forward, play animation
            animator.SetTrigger("WalkTrigger");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // Player moving left, play left strafe
            animator.SetTrigger("StrafeLeftTrigger");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Player moving right, play right strafe
            animator.SetTrigger("StrafeRightTrigger");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Player moving backward
            animator.SetTrigger("BackwardTrigger");

        }
        else
        {
            animator.SetTrigger("IdleTrigger");
        }

        if(canMove)
        {
            if(isRunning)
            {
                curSpeedX = runSpeed * Input.GetAxis("Vertical");
                curSpeedY = runSpeed * Input.GetAxis("Horizontal");
            }
            else
            {
                curSpeedX = walkSpeed * Input.GetAxis("Vertical");
                curSpeedY = walkSpeed * Input.GetAxis("Horizontal");
            }
        }

        float moveDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if(Input.GetButton("Jump") && canMove && controller.isGrounded)
        {
            moveDirection.y = jumpSpeed;
            animator.SetTrigger("JumpTrigger");
        }
        else
        {
            moveDirection.y = moveDirectionY;
        }

        if(!controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);

        if(canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        //------------------------------------------------------------------------

        //Player interaction
        if(Input.GetButtonDown("Interact"))
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.tag == "Interactable")
                {
                    hit.collider.gameObject.GetComponent<Interactable>().PlayerInteract();
                }
            }
        }
    }

    public void PauseMovement()
    {
        canMove = false;
    }

    public void UnpauseMovement()
    {
        canMove = true;
    }
}
