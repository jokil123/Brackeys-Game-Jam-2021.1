using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainershipHealthSystem : MonoBehaviour
{
    private int health = 100;
    public int Health
    {
        get { return health; }
        set {
            if (value <= 0)
            {
                health = 0;
                GameOver();

            } else
            {
                health = value;
            }
            UpdateUI();
        }
    }

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        slider.value = Health;
    }

    void GameOver()
    {
        Debug.Log("GameOver!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision!");
            Health -= 10;
        }
    }
}
