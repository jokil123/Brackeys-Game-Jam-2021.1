using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChild : MonoBehaviour
{
    public HashSet<GameObject> collisionObjects = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        collisionObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        collisionObjects.Remove(other.gameObject);
    }
}
