using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public enum HarvesterState
{
    Collect,
    Chase,
    Seek,
    LastSeen,
    LookAround
}

public class HarvesterMovement : MonoBehaviour
{

    public Transform[] patrolPoints;

    public Transform player;

    private Vector3 lastDestination;

    public bool shouldReturnLastDestination = false;

    public HarvesterState harvesterState;

    private NavMeshAgent agent;

    private bool isLookingAround = false;

    GameManager gameManager;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        harvesterState = HarvesterState.Collect;
        agent = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
    }

    private void Chase()
    {
        if (agent == null) return;
        shouldReturnLastDestination = true;
        lastDestination = agent.transform.position;
        agent.SetDestination(player.position);
    }

    private void Collect()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Vector3 randomDestination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;

            agent.destination = shouldReturnLastDestination ? lastDestination : randomDestination;

            if (shouldReturnLastDestination) shouldReturnLastDestination = false;
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            print(hit.position);
            return hit.position;
        } else
        {
            return RandomNavmeshLocation(radius);
        }
    }

    IEnumerator LookAround()
    {
        isLookingAround = true;
        agent.SetDestination(RandomNavmeshLocation(5f));
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);

        agent.SetDestination(RandomNavmeshLocation(5f));
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);

        agent.SetDestination(RandomNavmeshLocation(5f));
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);

        isLookingAround = false;
        harvesterState = HarvesterState.Collect;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.gameState == GameState.Chase || harvesterState == HarvesterState.Chase)
        {
            Chase();
        }

        if (harvesterState == HarvesterState.Collect)
        {
            Collect();
        }

        if (harvesterState == HarvesterState.LookAround && !isLookingAround)
        {
            StartCoroutine(LookAround());
        }

        animator.SetBool("isWalking", harvesterState != HarvesterState.Seek);
    }

}
