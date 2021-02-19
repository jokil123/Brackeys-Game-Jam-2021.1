using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public CinemachineVirtualCamera cmvcam;
    public Transform goal;
    public float goalFocusDuration = 3f;

    private Transform startingShip;

    void Start()
    {
        startingShip = cmvcam.Follow;
        GameMaster.gameController.controlIsEnabled = false;

        cmvcam.Follow = goal;
        cmvcam.LookAt = goal;
        StartCoroutine(EndCutscene(goalFocusDuration));
    }

    public IEnumerator EndCutscene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        cmvcam.Follow = startingShip;
        cmvcam.LookAt = startingShip;
        GameMaster.gameController.controlIsEnabled = true;
    }
}
