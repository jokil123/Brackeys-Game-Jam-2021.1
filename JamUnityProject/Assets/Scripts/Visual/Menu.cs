using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject controlls;

    void Start()
    {
        if (!Application.isEditor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        //Show / Close Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);

            if (!Application.isEditor)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        //Quit Game
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        //Close Controlls
        if (Input.anyKeyDown)
        {
            if (controlls.activeSelf)
            {
                controlls.SetActive(false);
            }
        }

        //Show Controlls
        if (Input.GetKeyDown(KeyCode.C))
        {
            controlls.SetActive(true);
            menu.SetActive(false);
        }
    }
}
