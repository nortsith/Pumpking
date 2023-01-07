using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody body;
    public float speed = 10f;
    public float jumpSpeed = 10f;
    public float speedMultiplier = 1f;
    public float jumpMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Move()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized * speed * speedMultiplier;
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
        Move();
        Jump();
        Face();
    }
}
