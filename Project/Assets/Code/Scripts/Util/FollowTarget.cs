using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    Rigidbody playerBody;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        _id = Shader.PropertyToID(shaderVariableName);
    }

    public string shaderVariableName = "_DistanceToCamera";
    private Vector3 _distanceObjectToCamera;
    private int _id;
    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * 10f);

        _distanceObjectToCamera = Camera.main.transform.position - this.transform.position;
        Shader.SetGlobalFloat(_id, _distanceObjectToCamera.magnitude);
    }
}
