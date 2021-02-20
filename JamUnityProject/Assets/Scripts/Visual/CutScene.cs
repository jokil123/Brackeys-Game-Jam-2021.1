using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public CinemachineVirtualCamera cmvcam;
    public Transform goal;
    public float goalFocusDuration = 3f;

    public bool showControlls = false;

    private Transform startingShip;

    public GameObject canvasMain;
    public GameObject canvasControlls;

    void Start()
    {
        startingShip = cmvcam.Follow;
        GameMaster.gameController.controlIsEnabled = false;

        cmvcam.Follow = goal;
        cmvcam.LookAt = goal;
        StartCoroutine(EndCutscene(goalFocusDuration));

    }

    void Update()
    {
        if (showControlls && Input.anyKeyDown)
        {
            showControlls = false;
            canvasMain.SetActive(true);
            canvasControlls.SetActive(false);
        }
    }

    public IEnumerator EndCutscene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        cmvcam.Follow = startingShip;
        cmvcam.LookAt = startingShip;
        GameMaster.gameController.controlIsEnabled = true;

        if (showControlls)
        {
            canvasControlls.SetActive(true);
            canvasMain.SetActive(false);
        }
    }
}
