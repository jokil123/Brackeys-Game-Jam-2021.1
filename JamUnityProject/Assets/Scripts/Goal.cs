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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ship)
        {
            inGoal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ship)
        {
            inGoal = false;
        }
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
