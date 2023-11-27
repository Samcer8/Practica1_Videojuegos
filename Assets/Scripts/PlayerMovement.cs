using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private KeyCode runKey;

    [SerializeField, Range(1, 5)]
    private float moveSpeed, extraSpeed;

    [SerializeField, Range(20, 360)]
    private float rotationSpeed;

    [SerializeField]
    private float jumpForce, runAddedSpeed, takenSpeed;
    private float jumpAddedForce;

    [SerializeField]
    private float rotationAngleLimitX = 40;

    [SerializeField]
    private bool isXInverted, isYInverted;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float gravity, verticalVelocity, terminalVelocity;

    private int inversionX => (isXInverted ? -1 : 1);
    private int inversionY => (isYInverted ? -1 : 1);

    private CharacterController m_characterController;
    private Vector3 m_PlayerCameraRotation;
    private Vector2 m_PlayerYRotationVector;

    private void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_PlayerCameraRotation = Vector3.zero;
        m_PlayerYRotationVector = Vector2.zero;
        cameraTransform = Camera.main?.transform;
        RemoveJumpForce();
        RemoveMoveSpeed();
        RecoverSpeed();
    }

    private void Update()
    {
        if (!m_characterController.isGrounded)
        {
            ApplyGravity();
        }
        else
        {
            if(Input.GetAxisRaw("Jump") > 0.5f)
            {
                verticalVelocity = jumpForce + jumpAddedForce;
            }
            else
            {
                verticalVelocity = -1;
            }
        }
        Vector3 movementDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        movementDirection.Normalize();
        m_characterController.Move(movementDirection * (takenSpeed * (extraSpeed * (moveSpeed * Time.deltaTime))) + Vector3.up * (verticalVelocity * Time.deltaTime));

        m_PlayerCameraRotation.x = Input.GetAxis("Mouse Y") * inversionX;
        m_PlayerCameraRotation.y = Input.GetAxis("Mouse X") * inversionY;

        Vector3 rotationVector = new Vector3(m_PlayerCameraRotation.x, 0, 0) * (rotationSpeed * Time.deltaTime);
        cameraTransform.Rotate(rotationVector);

        float angleTransformation = (cameraTransform.localEulerAngles.x > 180) ? cameraTransform.localEulerAngles.x -360 : cameraTransform.localEulerAngles.x;

        rotationVector = new Vector3(Mathf.Clamp(angleTransformation, -rotationAngleLimitX, rotationAngleLimitX), 0, 0);
        cameraTransform.localEulerAngles = (rotationVector);

        

        m_PlayerYRotationVector.y = m_PlayerCameraRotation.y;
        transform.Rotate(m_PlayerYRotationVector * (rotationSpeed * Time.deltaTime));

        if (Input.GetKey(runKey))
        {
            m_characterController.Move(movementDirection * runAddedSpeed * (extraSpeed * (moveSpeed * Time.deltaTime)) + Vector3.up * (verticalVelocity * Time.deltaTime));
        }
    }

    public void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        if(verticalVelocity < -terminalVelocity)
        {
            verticalVelocity = -terminalVelocity;
        }
    }

    public void AddJumpForce(float addedForce)
    {
        jumpAddedForce = addedForce;
    }

    public void RemoveJumpForce()
    {
        jumpAddedForce = 0;
    }

    public void AddMoveSpeed(float addedSpeed)
    {
        extraSpeed = addedSpeed;
    }

    public void RemoveMoveSpeed()
    {
        extraSpeed = 1;
    }

    public void TakeSpeed(float speed)
    {
        takenSpeed = speed;
    }

    public void RecoverSpeed()
    {
        takenSpeed = 1;
    }
}
