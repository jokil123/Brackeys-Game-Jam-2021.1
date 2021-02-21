using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private GameObject ship;
    
    [SerializeField]
    private float parkingTime;
    private float parkingTimeCurrent;

    [SerializeField]
    private Animation clearAnim;

    [SerializeField]
    private AudioSource clearSound;
    private bool playClearSound = true;

    [SerializeField]
    private Animation goalSlideranim;

    [SerializeField]
    private Slider slider;

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


    void Start()
    {
        slider.maxValue = parkingTime;
        slider.value = 0;
        slider.gameObject.SetActive(false);
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
            if (!slider.gameObject.activeSelf)
            {
                slider.gameObject.SetActive(true);
                goalSlideranim.Play();
            }

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
            if (slider.gameObject.activeSelf)
            {
                slider.gameObject.SetActive(false);
            }
        }


        if (parkingTimeCurrent <= 0)
        {
            InGoal();
        }
    }

    private void Countdown() {
        parkingTimeCurrent -= Time.deltaTime;
        slider.value = parkingTimeCurrent;
        //Debug.Log(parkingTimeCurrent);
    }

    private void InGoal()
    {
        Debug.Log("Level Win");
        clearAnim.Play();
        if (!clearSound.isPlaying && playClearSound)
        {
            clearSound.Play();
            playClearSound = false;
        }
        StartCoroutine(LoadNextScene());
    }

    public IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2.417f);
        SceneManager.LoadScene(nextLevelIndex);
    }
}
