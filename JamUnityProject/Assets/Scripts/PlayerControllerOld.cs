using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOld : MonoBehaviour
{
    private Vector2 inputVector;
    private Vector2 moveVector;

    private Rigidbody playerRigidbody;

    public float movementStrength;

    public bool useSecondaryControllScheme;

    private void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update() // Uff yeah this needs improving
    {
        if (useSecondaryControllScheme)
        {
            float x = 0;
            float y = 0;

            if (Input.GetKey(KeyCode.D))
            {
                x++;
            }
            if (Input.GetKey(KeyCode.A))
            {
                x--;
            }
            if (Input.GetKey(KeyCode.W))
            {
                y++;
            }
            if (Input.GetKey(KeyCode.S))
            {
                y--;
            }

            inputVector = new Vector2(x, y);
            //Debug.Log(inputVector);
        }
        else
        {
            inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

    private void FixedUpdate()
    {
        Move(inputVector, movementStrength);
    }

    private void Move(Vector2 direction, float strength)
    {
        playerRigidbody.AddForce(Utility.V2toV3(direction) * strength);
    }
}
