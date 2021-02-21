using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerShips = new List<GameObject>();
    public int shipIndex = 0;
    public CinemachineVirtualCamera cinemachine;
    public bool controlIsEnabled = true;

    public AudioSource switchShipSound;

    private void Awake()
    {
        GameMaster.gameController = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Switch") && controlIsEnabled)
        {
            SwitchPlayer();
        }

        DeveloperOptions();
    }

    void SwitchPlayer()
    {
        playerShips[shipIndex].GetComponent<PlayerController>().isActive = false;
        playerShips[shipIndex].GetComponent<PlayerController>().particles.emissionRate = 0;
        switchShipSound.Play();
        shipIndex++;
        if (shipIndex >= playerShips.Count)
        {
            shipIndex = 0;
        }

        playerShips[shipIndex].GetComponent<PlayerController>().isActive = true;
        Debug.Log("switched Ship");
        SwitchCamera();
    }

    private void SwitchCamera()
    {
        cinemachine.Follow = playerShips[shipIndex].transform;
        cinemachine.LookAt = playerShips[shipIndex].transform;
    }


    private void DeveloperOptions()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            Debug.Log("Switched to scene 1");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            Debug.Log("Switched to scene 2");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            Debug.Log("Switched to scene 3");
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
            Debug.Log("Switched to scene 4");
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
            Debug.Log("Switched to scene 5");
        }



        if (Input.GetKeyDown(KeyCode.F6))
        {
            PlayerController[] playerControllers;
            playerControllers = FindObjectsOfType<PlayerController>();
            
            foreach (PlayerController item in playerControllers)
            {
                item.speed = 10;
                item.turnspeed = 1;
            }

            Debug.Log("SPEDO MODO  ON");
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            ContainershipHealthSystem[] containerships;
            containerships = FindObjectsOfType<ContainershipHealthSystem>();

            foreach (ContainershipHealthSystem item in containerships)
            {
                Destroy(item.gameObject);
            }

            Debug.Log("Deleted Cargo Ship");
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            ContainershipHealthSystem[] containerships;
            containerships = FindObjectsOfType<ContainershipHealthSystem>();

            foreach (ContainershipHealthSystem item in containerships)
            {
                item.Health += 10;
            }

            Debug.Log("Added 10 Health");
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            ContainershipHealthSystem[] containerships;
            containerships = FindObjectsOfType<ContainershipHealthSystem>();

            foreach (ContainershipHealthSystem item in containerships)
            {
                item.Health -= 10;
            }

            Debug.Log("Removed 10 Health");
        }
    }
}
