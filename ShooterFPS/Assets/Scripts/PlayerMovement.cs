using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ajustes")]
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float sensitivity = 0.001f; // Prueba con 0.02 o 0.01
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;

    private bool isRunning = false;
    private bool isCrouching = false;
    private float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        currentSpeed = walkSpeed;
    }

    // --- Inputs del Player Input Component ---
    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();
    public void OnLook(InputValue value) => lookInput = value.Get<Vector2>();

    public void OnSprint(InputValue value) => isRunning = value.isPressed;

    public void OnCrouch(InputValue value)
    {
        isCrouching = value.isPressed;
        // Cambiar altura del personaje
        controller.height = isCrouching ? 1f : 2f;
        currentSpeed = isCrouching ? walkSpeed * 0.5f : walkSpeed;
    }

    void Update()
    {
        float mouseX = lookInput.x * sensitivity * Time.deltaTime * 100f;
        float mouseY = lookInput.y * sensitivity * Time.deltaTime * 100f;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // 2. Movimiento
        currentSpeed = isRunning && !isCrouching ? runSpeed : (isCrouching ? walkSpeed * 0.5f : walkSpeed);

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.SimpleMove(move * currentSpeed);
    }
}