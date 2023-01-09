using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float duration = 5.0f;
    Animator animator;
    public enum Option
    {
        Bear = 1,
        Sticky = 2
    }

    public Option TrapType;

    private Movement playerMovement;
    private float timer;
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<Movement>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TrapTimer()
    {
        print("Trapped!");
        timer = duration;
        animator.SetTrigger("Triggered");
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }

        playerMovement.movementState = MovementState.Default;
        print("Not Trapped!");
        Destroy(gameObject);
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

    private void OnTriggerEnter(Collider trappedObject)
    {
        if (trappedObject.tag != "Player") return;
        boxCollider.enabled = false;
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
