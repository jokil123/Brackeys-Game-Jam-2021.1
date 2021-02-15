using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emmision : MonoBehaviour
{
    public ParticleSystem particles;
    private bool isEmmiting = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Help");
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isEmmiting)
            {
                particles.emissionRate = 0;

            } else
            {
                particles.emissionRate = 10;
            }
            isEmmiting = !isEmmiting;
        }
    }
}
