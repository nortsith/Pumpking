using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum HarvesterState
{
    Collect,
    Chase
}

public class HarvesterMovement : MonoBehaviour
{

    public Transform player;

    public float followDistance = 15.0f;

    public float followSpeed = 2.0f;

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

        agent.SetDestination(player.position);
    }

    public void Collect()
    {
        if (agent.hasPath)
        {
            agent.destination = Vector3.zero;
        }
        // Patrol
        // print("Patrolling");
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
