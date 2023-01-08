using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

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
    Movement playerMovement;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        harvester = FindObjectOfType<HarvesterMovement>();
        playerMovement = FindObjectOfType<Movement>();
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
        Debug.DrawRay(transform.position, directionToPlayer, Color.yellow);

        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit) && angle <= sightAngle)
        {
            if (hit.transform.tag == "Player" && playerMovement.isSpottable)
            {
                isTargetInSight = true;
                harvester.harvesterState = HarvesterState.Chase;
            }
            else if (!hasTimerStarted)
            {
                StartCoroutine(OutOfSightTimer());
            }
        }
        else
        {
            if (!hasTimerStarted && isTargetInSight)
            {
                StartCoroutine(OutOfSightTimer());
            }
        }
            
    }
}
