using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput playerInput;

    public Transform playerBody;

    public Transform playerHead;
    private Vector2 turnInput;
    public float turnSpeed = 5f;
    public float lookXLimit = 45.0f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    public float maxSpeed = 10f;
    private Vector2 moveInput;
    public float moveSpeed = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 headForward = playerHead.forward;
        Vector3 headRight = playerHead.right;

        headForward.y = 0f;
        headRight.y = 0f;

        Vector3 relativeForward = headForward * moveInput.y;
        Vector3 relativeRight = headRight * moveInput.x;

        Vector3 moveDir = relativeForward + relativeRight;

        rb.AddForce(new Vector3(moveDir.x, 0, moveDir.z) * moveSpeed);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    private void LateUpdate()
    {
        float mouseX = turnInput.x;
        float mouseY = turnInput.y;

        rotationY += mouseX;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerHead.localRotation = Quaternion.Lerp(playerHead.localRotation, Quaternion.Euler(rotationX, 0, 0), turnSpeed * Time.fixedDeltaTime);

        playerBody.rotation = Quaternion.Lerp(playerBody.rotation, Quaternion.Euler(0f, rotationY, 0f), turnSpeed * Time.fixedDeltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<Vector2>();
    }
}
