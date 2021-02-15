using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private GameObject ship;
    
    [SerializeField]
    private float parkingTime;
    private float parkingTimeCurrent;

    private bool inGoal;


    public HashSet<GameObject> collisionObjects = new HashSet<GameObject>();


    private void OnTriggerEnter(Collider other)
    {
        collisionObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        collisionObjects.Remove(other.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if (inGoal)
        {
            Countdown();
        }
        else
        {
            parkingTimeCurrent = parkingTime;
        }

        if (parkingTimeCurrent <= 0)
        {
            Debug.LogWarning("Level Win");
        }
    }

    private void Countdown()
    {
        parkingTimeCurrent -= Time.deltaTime;
        //Debug.Log(parkingTimeCurrent);
    }
}
