using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTraps : MonoBehaviour
{
    HarvesterMovement movement;
    public Trap trap;
    private int minTime = 20;
    private int maxTime = 30;
    private bool invokePending;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<HarvesterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.harvesterState == HarvesterState.Collect && !invokePending)
        {
            float randomTime = Random.Range(minTime, maxTime);
            Invoke("PlaceTrap", randomTime);
            invokePending = true;
        }
    }

    void PlaceTrap()
    {
        Instantiate(trap, transform.position, Quaternion.identity);
        invokePending = false;
    }
}
