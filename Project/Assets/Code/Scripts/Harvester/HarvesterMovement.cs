using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public enum HarvesterState
{
    Collect,
    Chase
}

public class HarvesterMovement : MonoBehaviour
{

    public Transform[] patrolPoints;

    public Transform player;

    private Vector3 lastDestination;

    public bool shouldReturnLastDestination = false;
    
    public HarvesterState harvesterState;

    private NavMeshAgent agent;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        harvesterState = HarvesterState.Collect;
        agent = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();

    }

    private void Chase()
    {
        if (agent == null) return;
        shouldReturnLastDestination = true;
        lastDestination = agent.transform.position;
        agent.SetDestination(player.position);
    }

    public void Collect()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Vector3 randomDestination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;

            agent.destination = shouldReturnLastDestination ? lastDestination : randomDestination;

            if (shouldReturnLastDestination) shouldReturnLastDestination = false;
        }
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
    }

}
