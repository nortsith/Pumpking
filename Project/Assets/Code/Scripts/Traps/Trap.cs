using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float slowFactor = 0.5f;
    public float duration = 5.0f;

    private bool applyForce = false;
    private bool isTriggered = false;
    private float elapsedTime = 0.0f;
    private Rigidbody applyForceRigidbody;

    public enum Option
    {
        Bear = 1,
        Sticky = 2
    }

    public Option TrapType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (applyForce && !isTriggered)
        {
            if (TrapType == Option.Sticky)
            {
                Sticky();
            }
        }
       
    }

    void Sticky()
    {
        Vector3 originalVelocity = applyForceRigidbody.velocity;
        applyForceRigidbody.velocity = originalVelocity * slowFactor;
    }
    
    private void OnTriggerEnter(Collider trappedObject) {
        print(trappedObject.gameObject.tag);
        applyForceRigidbody = trappedObject.gameObject.GetComponent<Rigidbody>();
        applyForce = true;
        isTriggered = true;
    }
}
