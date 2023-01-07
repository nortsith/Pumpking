using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody body;
    public float speed = 10f;
    public float jumpSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Move()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized * speed;
        move.y = body.velocity.y;
        body.velocity = move;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.velocity += Vector3.up * jumpSpeed;
        }
    }

    void Face()
    {
        if (body.velocity.magnitude >= 0.1f)
        {
            Vector3 rotation = new Vector3(body.velocity.x, 0, body.velocity.z);
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
