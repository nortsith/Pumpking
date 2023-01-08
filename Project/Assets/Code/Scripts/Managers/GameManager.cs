using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Collect,
    Chase
}

public class GameManager : MonoBehaviour
{
    Transform player;
    Movement playerMovement;
    public float timeRemaining = 666;
    public float chaseTime = 20;
    public float chaseTimeRemaining;
    public GameState gameState = GameState.Collect;
    public int boostsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerMovement = player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
    }

    IEnumerator ChaseTimer()
    {
        print("Chase!");
        gameState = GameState.Chase;
        chaseTimeRemaining = chaseTime;
        playerMovement.speedMultiplier += .1f;
        playerMovement.jumpMultiplier += .05f;

        while (chaseTimeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            chaseTimeRemaining--;
        }

        gameState = GameState.Collect;
        print("Collect!");

        yield return null;
    }

    public void BoostCollected()
    {
        boostsCollected++;
        if (boostsCollected % 4 == 0)
        {
            StartCoroutine(ChaseTimer());
        }
    }

    void CountDown()
    {
        if (gameState != GameState.Collect) return;

        if (timeRemaining > 0)
            timeRemaining -= Time.deltaTime;
        else
            LoseGame();
    }

    public void WinGame()
    {
        //Play win game cutscene
        playerMovement.enabled = false;
    }

    void LoseGame()
    {
        //LoseGame Sequence
        playerMovement.enabled = false;
    }
}
