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
    private float particleSpeed = 1;

    public float speed;
    public float turnspeed;

    private float soundVolume = 0f;

    [SerializeField]
    private AudioSource shipDrivingSound;

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
        shipDrivingSound.volume = soundVolume;
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
                particles.startSpeed = particleSpeed;
            } else
            {
                particles.startSpeed = -particleSpeed;
            }

            if (soundVolume < 1)
            {
                soundVolume += 0.1f;
            }
        }
        else
        {
            particles.emissionRate = 0;

            if (soundVolume > 0)
            {
                soundVolume -= 0.1f;
            }
        }

    }

    void TurnTorque(float torque)
    {
        Vector3 torqueDirection = new Vector3(0, torque, 0);
        playerRigidbody.AddTorque(torqueDirection);
    }
}
