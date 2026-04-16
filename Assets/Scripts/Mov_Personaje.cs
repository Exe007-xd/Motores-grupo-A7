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
    [SerializeField] private float _sprintSpeed = 14f;
        
    [Header("Salto")]
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _groundHeight = 0f;

    [Header("Rotación")]
    [SerializeField] private float _rotateSpeed = 100f;

    [Header("Camara: modo hijo")]
    [SerializeField] private Transform _cameraTransform;


    //------------------
    //Variables propias
    //------------------

    private CharacterController _controller;
    private float _speed;
    private Vector2 _move;
    private float _rotate;
    private float _verticalVelocity;
    private bool _isGrounded = true;








    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speed = _normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
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
        float mouseX = _rotate * _rotateSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
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

    
}
