using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    LayerMask layerMask;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Player", "Ignore Raycast");
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
