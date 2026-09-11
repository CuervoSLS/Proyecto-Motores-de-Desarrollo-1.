using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float _normalSpeed = 5f;
    [SerializeField] private float _sprintSpeed = 15f;

    [Header("Rotacion")]

    [SerializeField] private float _rotateSpeed = 100f;

    [Header("Salto y gravedad")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _originalGravity;

    [Header("Carama (modo hijo")]

    [SerializeField] private Transform _camaraTransform;
    [Header("Objeto a instanciar")]
    [SerializeField] GameObject GameObjectPrefab;

    [SerializeField] private bool _liane = false;
    public CharacterController _controller;
    private float _speed;
    private Vector2 _move;
    private float _rotate;
    private Vector2 _look;
    private float _verticalVelocity;
    private float _pitch;

    private void Awake()
    {
        _controller = GetComponent <CharacterController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _speed = _normalSpeed;
        _originalGravity = _gravity;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleLook();
    }
    private void FixedUpdate()
    {
        
    }
    private void HandleMovement()
    {
        Vector3 move = transform.forward * _move.y + transform.right * _move.x;

        move = move.normalized * _speed;

        _verticalVelocity += _gravity * Time.deltaTime;
        move.y = _verticalVelocity;
        _controller.Move(move * Time.deltaTime);
    }
    private void HandleRotation()
    {
        float rotation = _rotate * _rotateSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
    private void HandleLook()
    {
        float mouseX = _look.x * _rotateSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }
    
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (_liane == false)
        {
            _move = context.ReadValue<Vector2>();
        }
        else
        {
            Vector3 movement = Vector3.zero;
            if (Keyboard.current.wKey.isPressed)
            {
                movement = Vector3.up;
            }
            if (Keyboard.current.wKey.isPressed)
            {
                movement = Vector3.up;
            }
             _controller.Move(movement * _speed * Time.deltaTime);
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        _look = context.ReadValue<Vector2>();
    }
    public void OnRotate(InputAction.CallbackContext context)
    {
        _rotate = context.ReadValue<float>();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            if(_controller.isGrounded == true)
            {
                _speed = _sprintSpeed;
            }
        if (context.canceled)
            if (_controller.isGrounded == true)
            {
                _speed = _normalSpeed;
            }
                
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && _controller.isGrounded || context.performed && _liane == true)
        {
            _verticalVelocity = Mathf.Sqrt(_jumpForce * -2f * _gravity);
        }
    }
    public void OnLiane()
    {
        _gravity = 0;
        _liane = true;
    }
    public void OffLiane()
    {
        _gravity = _originalGravity;
        _liane = false;
    }
}

