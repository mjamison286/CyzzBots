using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    Rigidbody rb;

    InputAction moveAction;
    InputAction lookAction;

    public float speed;
    float pitch = 0f;
    float maxPitch = 89f;
    public float lookSensitivity;

    Camera cam;

    private void Awake() 
    {
        characterController = gameObject.GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();

        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
    }

    private void FixedUpdate() 
    {
        Vector2 moveVal = moveAction.ReadValue<Vector2>();

        Vector3 moveDir = new Vector3(moveVal.x, 0, moveVal.y);

        if(Mathf.Abs(rb.linearVelocity.magnitude) < 10)
        {
            rb.AddForce(moveDir * speed);
        }
    }

    private void Update() 
    {
        ProcessLook();
    }

    void ProcessLook()
    {
        Vector2 mouseDeltas = lookAction.ReadValue<Vector2>();

        pitch += mouseDeltas.y * lookSensitivity;
        pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);

        cam.transform.localRotation = Quaternion.Euler(-pitch, 0, 0);
        transform.Rotate(0, mouseDeltas.x * lookSensitivity, 0);
    }
}
