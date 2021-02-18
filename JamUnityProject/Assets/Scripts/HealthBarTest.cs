using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarTest : MonoBehaviour
{
    public Slider primarySlider;
    public Slider secondarySlider;
    public Text hpText;

    public int hp = 100;


    // Start is called before the first frame update
    void Start()
    {
        primarySlider.value = hp;
        secondarySlider.value = hp;
        hpText.text = $"HP: {hp}";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            hp -= 10;
            if (hp <= 0)
            {
                hp = 100;
                secondarySlider.value = hp;
            }
            hpText.text = $"HP: {hp}";
            primarySlider.value = hp;
            
        }

        //update secondary slider

        if (secondarySlider.value > primarySlider.value)
        {
            secondarySlider.value -= 0.1f;
        }
    }
}
