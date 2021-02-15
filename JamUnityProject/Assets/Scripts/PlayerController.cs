using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    public bool isActive;

    [SerializeField]
    private float forceScale;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float turnspeed;

    void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            Thrust(Input.GetAxis("Vertical") * speed);
            TurnTorque(Input.GetAxis("Horizontal") * turnspeed);
        }
    }

    void Thrust(float strength)
    {
        Vector3 thrustDireciton = transform.TransformVector(Vector3.right) * strength;
        Utility.LineRel(gameObject, thrustDireciton, Color.green);
        playerRigidbody.AddForce(thrustDireciton);
    }

    void TurnTorque(float torque)
    {
        Vector3 torqueDirection = new Vector3(0, torque, 0);
        playerRigidbody.AddTorque(torqueDirection);
    }
}
