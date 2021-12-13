using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speedMove;
    public float jumpHeight;
    private PlayerInputActions playerInputActions;
    private InputAction movement;
    private Rigidbody rigidbody;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        movement = playerInputActions.Player.Movement;
        movement.performed += DoMove;
        movement.Enable();

        playerInputActions.Player.Jump.performed += DoJump;
        playerInputActions.Enable();
    }

    private void DoMove(InputAction.CallbackContext ctx)
    {
        Vector2 inputVector = ctx.ReadValue<Vector2>();
        rigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speedMove, ForceMode.Force);
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y));
        rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
        
        print("Jump!");
    }

    private void OnDisable()
    {
        movement.Disable();
        playerInputActions.Player.Jump.Disable();
    }
    private void FixedUpdate()
    {
        Debug.Log(movement.ReadValue<Vector2>());
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
