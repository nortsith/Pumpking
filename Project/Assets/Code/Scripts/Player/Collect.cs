using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    Collectible[] collectibles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            collectibles = FindObjectsOfType<Collectible>();
            Collectible nearestCollectible = GetNearestCollectible();
            if (nearestCollectible == null) return;
            if (Vector3.Distance(nearestCollectible.transform.position, transform.position) < 3)
                nearestCollectible.Collect();
        }
    }

    Collectible GetNearestCollectible()
    {
        Collectible nearest = null;

        foreach (Collectible item in collectibles)
        {
            if (nearest == null) nearest = item;
            else if (Vector3.Distance(item.transform.position, transform.position) < Vector3.Distance(nearest.transform.position, transform.position)) nearest = item;
        }

        return nearest;
    }
}
