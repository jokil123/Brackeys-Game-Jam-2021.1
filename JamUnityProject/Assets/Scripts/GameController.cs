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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchPlayer();
        }
    }

    void SwitchPlayer()
    {
        playerShips[shipIndex].GetComponent<PlayerController>().isActive = false;
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
}
