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
    public Slider secondarySlider;
    public Text hpText;
    public Text hpTextSecondary;
    public Animation gameOverAnim;
    public AudioSource impactSound;

    void Start()
    {
        UpdateUI();
        secondarySlider.value = Health;
    }

    void Update()
    {
        if (secondarySlider.value > slider.value)
        {
            secondarySlider.value -= 0.1f;
        }
    }

    void UpdateUI()
    {
        slider.value = Health;
        hpText.text = $"HP: {Health}";
        hpTextSecondary.text = $"HP: {Health}";
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
            Health -= 10;
            impactSound.Play();
        }
    }

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
