using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class Mov_Personaje : MonoBehaviour
{
    //---------------------
    //Configuracion
    //---------------------



    [Header("Movimiento")]
    [SerializeField] private float _normalSpeed = 7f;
    [SerializeField] private float _sprintSpeed = 10f;

    [Header("Gravedad")]
    [SerializeField] private float _gravity = -9.8f;
   

   

    [Header("Camara: modo hijo")]
    [SerializeField] private Transform _cameraTransform;
    private float _pitch;
    private Vector2 _look;
    private float _lookSensitivity = 3f;

    //------------------
    //Variables propias
    //------------------

    private CharacterController _controller;
    private float _speed;
    private Vector2 _move;
    private float _rotate;
    private float _verticalVelocity;
  

    //-------
    //Metodos
    //-------


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    void Start()
    {
        _speed = _normalSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
    }


    private void HandleMovement()
    {
        Vector3 move = transform.forward * _move.y + transform.right * _move.x;

        move = move.normalized * _speed;

        if (_controller.isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }

        _verticalVelocity += _gravity * Time.deltaTime;
        move.y = _verticalVelocity;
        _controller.Move(move * Time.deltaTime);
    }

    private void HandleLook()
    {
        float mouseX = _look.x * _lookSensitivity * Time.deltaTime;
        float mouseY = _look.y * _lookSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        _cameraTransform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _speed = _sprintSpeed;
        }
        else if (context.canceled)
        {
            _speed = _normalSpeed;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
        _look = context.ReadValue<Vector2>();
    }

}
