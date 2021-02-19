using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    public ParticleSystem particles;
    public float emmisionRate = 10f;

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
        if (isActive && GameMaster.gameController.controlIsEnabled)
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

        if (strength != 0)
        {
            particles.emissionRate = emmisionRate;
            if (strength>0)
            {
                particles.startSpeed = 1;
            } else
            {
                particles.startSpeed = -1;
            }
        }
        else
        {
            particles.emissionRate = 0;
        }

    }

    void TurnTorque(float torque)
    {
        Vector3 torqueDirection = new Vector3(0, torque, 0);
        playerRigidbody.AddTorque(torqueDirection);
    }
}
