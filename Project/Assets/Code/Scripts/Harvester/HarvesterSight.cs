using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class HarvesterSight : MonoBehaviour
{
    public float sightRange = 10f;
    public float sightAngle = 45f;
    public float eyeHeight = 1f;
    public float outOfSightThreshold = 5f;
    public LayerMask targetLayer;

    private HarvesterMovement harvester;

    bool hasTimerStarted = false;
    Movement playerMovement;
    public Transform target;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        harvester = gameObject.GetComponent<HarvesterMovement>();
        playerMovement = FindObjectOfType<Movement>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private IEnumerator OutOfSightTimer()
    {
        hasTimerStarted = true;

        harvester.shouldReturnLastDestination = false;
        harvester.harvesterState = HarvesterState.LastSeen;

        yield return new WaitUntil(() => agent.remainingDistance < 0.5f);
        harvester.harvesterState = HarvesterState.Seek;

        yield return new WaitForSeconds(outOfSightThreshold);

        harvester.harvesterState = HarvesterState.LookAround;

        hasTimerStarted = false;
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
            if (hit.transform.tag == "Player" && playerMovement.isSpottable && !hasTimerStarted)
            {
                harvester.harvesterState = HarvesterState.Chase;
                StopAllCoroutines();
            }
            else if (!hasTimerStarted && harvester.harvesterState == HarvesterState.Chase)
            {
                StartCoroutine(OutOfSightTimer());
            }
            else if (hit.transform.tag == "Player" && playerMovement.isSpottable && hasTimerStarted)
            {
                StopAllCoroutines();
                hasTimerStarted = false;
            }
        }
        else
        {
            if (!hasTimerStarted && harvester.harvesterState == HarvesterState.Chase)
            {
                StartCoroutine(OutOfSightTimer());
            }
        }

    }
}
