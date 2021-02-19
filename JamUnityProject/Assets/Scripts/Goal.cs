using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private GameObject ship;
    
    [SerializeField]
    private float parkingTime;
    private float parkingTimeCurrent;

    [SerializeField]
    private Animation clearAnim;

    private bool inGoal;

    [SerializeField]
    private int nextLevelIndex;


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
        bool shipInside = false;
        foreach (GameObject item in collisionObjects)
        {
            if (item == ship)
            {
                shipInside = true;
            }
        }

        if (shipInside)
        {
            if (!gameObject.GetComponent<BoundaryBox>().IsColliding(ship))
            {
                Countdown();
            }
            else
            {
                parkingTimeCurrent = parkingTime;
            }
        }
        else
        {
            parkingTimeCurrent = parkingTime;
        }


        if (parkingTimeCurrent <= 0)
        {
            InGoal();
        }
    }

    private void Countdown()
    {
        parkingTimeCurrent -= Time.deltaTime;
        //Debug.Log(parkingTimeCurrent);
    }

    private void InGoal()
    {
        Debug.Log("Level Win");
        clearAnim.Play();
        StartCoroutine(LoadNextScene());
    }

    public IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2.417f);
        SceneManager.LoadScene(nextLevelIndex);
    }
}
