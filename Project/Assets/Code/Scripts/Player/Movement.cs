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
        if (movementState == MovementState.Immobilize)
        {
            body.velocity = Vector3.zero;
            return;
        };

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        float multiplier = speedMultiplier * (movementState == MovementState.Slowed ? .5f : 1f);

        Vector3 move = new Vector3(horizontal, 0, vertical).normalized * speed * multiplier;
        move.y = body.velocity.y;
        body.velocity = move;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if (inputTimer > 0)
            Move();
        Jump();

        isSpottable = body.velocity.sqrMagnitude > .3f;
        //Face();
    }
}
