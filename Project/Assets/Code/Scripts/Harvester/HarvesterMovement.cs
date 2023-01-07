using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HarvesterMovement : MonoBehaviour
{

    public Transform player;

    public float followDistance = 15.0f;

    public float followSpeed = 2.0f;

    private NavMeshAgent agent;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        print(gameManager.gameState);
        if (gameManager.gameState == GameState.Chase)
        {
            agent.SetDestination(player.position);
        }
        
        
    }

}
