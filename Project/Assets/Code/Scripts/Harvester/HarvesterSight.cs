using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterSight : MonoBehaviour
{
    public float sightRange = 10f;

    public float outOfSightTimer = 5f;

    public LayerMask targetLayer;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= sightRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, target.position - transform.position, out hit, sightRange, targetLayer))
            {
                if (hit.transform == target)
                {
                    print("target in sight");
                }
            }
        }
    }
}
