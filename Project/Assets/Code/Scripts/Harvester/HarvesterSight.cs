using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterSight : MonoBehaviour
{
    public float sightRange = 10f;
    public float sightAngle = 45f;
    public float eyeHeight = 1f;
    public float outOfSightThreshold = 5f;
    public LayerMask targetLayer;

    private HarvesterMovement harvester;
    
    bool isTargetInSight = false;
    bool hasTimerStarted = false;
    Movement target;

    // Start is called before the first frame update
    void Start()
    {
        harvester = FindObjectOfType<HarvesterMovement>();
        target = FindObjectOfType<Movement>();
    }

    private IEnumerator OutOfSightTimer()
    {
        hasTimerStarted = true;

        yield return new WaitForSeconds(outOfSightThreshold);

        harvester.harvesterState = HarvesterState.Collect;
        hasTimerStarted = false;
        isTargetInSight = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = target.transform.position - transform.position;

        if (directionToPlayer.magnitude <= sightRange)
        {
            float angle = Vector3.Angle(transform.forward, directionToPlayer);

            if (angle <= sightAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up * eyeHeight, directionToPlayer, out hit, sightRange, targetLayer))
                {
                    if (hit.transform == target.transform && target.isSpottable)
                    {
                        isTargetInSight = true;
                        harvester.harvesterState = HarvesterState.Chase;
                    }
                }
                else
                {
                    if (!hasTimerStarted && isTargetInSight)
                    {
                        StartCoroutine(OutOfSightTimer());
                    }
                }
                Debug.DrawRay(transform.position + Vector3.up * eyeHeight, directionToPlayer, Color.yellow, 2, false);
            }
        }
    }
}
