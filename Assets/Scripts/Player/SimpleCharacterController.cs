namespace CommentParticipatorySystem.Examples
{
    using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
    public class SimpleCharacterController : MonoBehaviour
    {
        [SerializeField] float walkSpeed    = 2f;
        [SerializeField] float runSpeed     = 4f;
        [SerializeField] float jumpHeight   = 1.0f;
        [SerializeField] float gravity      = -9.81f;

        [Space]

        [SerializeField] new Camera camera      = null;
        [SerializeField] MouseLook mouseLook    = null;

        private Vector3 velocity = Vector3.zero;

        private bool isGrounded = false;
        private bool isRunning  = false;

        CharacterController controller = null;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            mouseLook = new MouseLook(); mouseLook.Init(transform);
        }

        private void Update()
        {
            if (Time.timeScale > Mathf.Epsilon)
            {
                mouseLook.LookRotation(transform, camera.transform);
            }
        }
        void FixedUpdate()
        {
            isGrounded = controller.isGrounded;
            DoMovement();
        }
        void DoMovement()
        {
            isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            if (isGrounded)
            {
                velocity.y = 0f;
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 move = transform.right * h + transform.forward * v;
            controller.Move(move * Time.deltaTime * (isRunning ? runSpeed : walkSpeed));

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}