using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float duration = 5.0f;
    public enum Option
    {
        Bear = 1,
        Sticky = 2
    }

    public Option TrapType;

    private Movement playerMovement;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TrapTimer()
    {
        print("Trapped!");
        timer = duration;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
        
        playerMovement.movementState = MovementState.Default;
        print("Not Trapped!");

        yield return null;
    }

    void Sticky()
    {
        playerMovement.movementState = MovementState.Slowed;
        StartCoroutine(TrapTimer());
    }

    void Bear()
    {
        playerMovement.movementState = MovementState.Immobilize;
        StartCoroutine(TrapTimer());
    }
    
    private void OnTriggerEnter(Collider trappedObject) {
        switch (TrapType)
        {
            case Option.Sticky:
                Sticky();
                return;
            case Option.Bear:
                Bear();
                return;
            default:
                return;
        }
        
    }
}
