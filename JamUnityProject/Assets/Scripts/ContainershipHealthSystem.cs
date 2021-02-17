using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Animation gameOverAnim;

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
        gameOverAnim.Play();
        GameMaster.gameController.controlIsEnabled = false;
        StartCoroutine(ReloadScene());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision!");
            Health -= 10;
        }
    }

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
