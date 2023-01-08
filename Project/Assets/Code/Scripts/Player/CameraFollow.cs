using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    Rigidbody playerBody;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerBody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPos = player.position + offset;
        desiredPos += playerBody.velocity;
        desiredPos.y = 0 + offset.y;
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * 2f);
    }
}
