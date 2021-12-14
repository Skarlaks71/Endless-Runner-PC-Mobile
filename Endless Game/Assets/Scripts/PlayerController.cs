using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speedMove;
    public float jumpHeight;
    public float jumpFall;
    public Transform[] lanes;
    public Transform body;
    public BoxCollider boxCol;
    private PlayerInputActions playerInputActions;
    private InputAction movement;
    public Rigidbody rigidbodyP;
    private int laneIndex = 2;
    private bool isJumping;
    private CustomGravity customGravity;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        body.transform.position = lanes[laneIndex].position;
        customGravity = body.GetComponent<CustomGravity>();
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
        Debug.Log(movement.ReadValue<Vector2>());

        Vector2 inputVector = ctx.ReadValue<Vector2>();
        int verifyPosition = (laneIndex + (int)inputVector.x);
        if (verifyPosition < 0 || verifyPosition >= lanes.Length)
        {
            return;
        }
        else
        {
            laneIndex = (laneIndex + (int)inputVector.x) % lanes.Length;

        StartCoroutine("Move", laneIndex);
        }
        
        //Vector3 newPos = new Vector3(lanes[laneIndex].position.x, body.transform.position.y, lanes[laneIndex].position.z);
        //body.transform.position = Vector3.Lerp(body.transform.position, newPos, speedMove * Time.deltaTime);
    }
    IEnumerator Move(int index)
    {
        movement.Disable();
        Vector3 referencePositionLane = new Vector3(lanes[index].position.x, body.transform.position.y, lanes[index].position.z);
        while((referencePositionLane - body.transform.position).magnitude > 0.4)
        {
            print((referencePositionLane - body.transform.position).magnitude);
            Vector3 newPos = new Vector3(lanes[index].position.x, body.transform.position.y, lanes[index].position.z);
            referencePositionLane = newPos;
            body.transform.position = Vector3.Lerp(body.transform.position, referencePositionLane, speedMove * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        movement.Enable();
        yield return null;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y*customGravity.gravityScale));
        rigidbodyP.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
        
        print("Jump!");
    }
    
    private void OnDisable()
    {
        movement.Disable();
        playerInputActions.Player.Jump.Disable();
    }
    private void FixedUpdate()
    {
        //Debug.Log(movement.ReadValue<Vector2>());
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    

}
