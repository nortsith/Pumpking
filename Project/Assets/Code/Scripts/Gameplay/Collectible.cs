using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Collectible : MonoBehaviour, ICollectible
{
    public new string name;
    public bool isWinCollectible = false;

    [Header("Particle")]
    public ParticleSystem collectedParticle;

    [Header("Sound")]
    public EventReference collectedSFX;

    GameManager gameManager;
    Transform player;
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        canvas = GetComponentInChildren<Canvas>();
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 5f && !canvas.gameObject.activeSelf) canvas.gameObject.SetActive(true);
        else if (Vector3.Distance(transform.position, player.position) >= 5f && canvas.gameObject.activeSelf) canvas.gameObject.SetActive(false);
    }

    public void Collect()
    {
        if (collectedParticle) collectedParticle.Play();

        RuntimeManager.PlayOneShot(collectedSFX, transform.position);

        if (!isWinCollectible) gameManager.BoostCollected();
        else gameManager.WinGame();

        Destroy(gameObject);
    }
}
