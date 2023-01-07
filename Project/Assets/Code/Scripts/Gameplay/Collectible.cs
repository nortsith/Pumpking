using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Collectible : MonoBehaviour, ICollectible
{
    public bool isSuccess = true;

    [Header("Particle")]
    public ParticleSystem collectedParticle;

    [Header("Sound")]
    public EventReference collectedSFX;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Collect()
    {
        if (collectedParticle) collectedParticle.Play();

        RuntimeManager.PlayOneShot(collectedSFX, transform.position);

        if (isSuccess) gameManager.SuccessfulAttempt();
        else gameManager.FailedAttempt();

        Destroy(gameObject);
    }
}
