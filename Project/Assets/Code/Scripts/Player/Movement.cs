using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    Default,
    Immobilize,
    Slowed,
}

public class Movement : MonoBehaviour
{
    Rigidbody body;
    public float speed = 10f;
    public float jumpSpeed = 10f;
    public float speedMultiplier = 1f;
    public float jumpMultiplier = 1f;
    public bool isSpottable = false;
    public LayerMask groundLayerMask;

    public MovementState movementState = MovementState.Default;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }


    public bool activeInput = false;
    public float inputTimer = .1f;
    void CheckInput()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (vertical == 0 && horizontal == 0)
        {
            if (inputTimer > 0) inputTimer -= Time.deltaTime;
        }
        else
        {
            inputTimer = .1f;
        }
    }

    void Move()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        float multiplier = speedMultiplier * (movementState == MovementState.Slowed ? .5f : 1f);

        Vector3 move = new Vector3(horizontal, 0, vertical).normalized * speed * multiplier;
        //move = Camera.main.transform.TransformDirection(move);
        move.y = body.velocity.y;
        body.velocity = move;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            body.velocity += Vector3.up * jumpSpeed * jumpMultiplier;
        }
    }

    void Face()
    {
        if (body.velocity.sqrMagnitude > 0.1f)
        {
            Vector3 rotation = new Vector3(body.velocity.x, 0, body.velocity.z);
            if (rotation != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(rotation, Vector3.up);
        }
    }

    public float spotTimer = 3f;
    void SpottableHandler()
    {
        if (body.velocity.sqrMagnitude < 2f)
        {
            if (spotTimer > 0)
            {
                spotTimer -= Time.deltaTime;
                isSpottable = true;
            }
            else
            {
                isSpottable = false;
            }

        }
        else
        {
            spotTimer = 3f;
            isSpottable = true;
        }
    }

    public bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        CheckInput();

        if (movementState != MovementState.Immobilize)
        {
            if (inputTimer > 0 && isGrounded)
                Move();
            Jump();
        }
        else
        {
            body.velocity = Vector3.zero + Vector3.up * body.velocity.y;
        }

        SpottableHandler();

        isGrounded = Physics.CheckSphere(transform.position + (transform.up * .3f), 2f, groundLayerMask);
        //Face();
    }
}
