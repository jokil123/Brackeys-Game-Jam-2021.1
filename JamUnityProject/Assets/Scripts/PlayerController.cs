using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    [SerializeField]
    private Transform thrustOffset;
    [SerializeField]
    private float forceScale;

    void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Vector3.zero, transform.TransformVector(Vector3.right), Color.white, Time.deltaTime, false);
    }

    void FixedUpdate()
    {
        Thrust(GetRelativeInputDirection(GetInputDirection()));
    }

    void Thrust(Vector2 direction)
    {
        Vector3 moveVector = Utility.V2toV3(direction) * forceScale;
        playerRigidbody.AddForceAtPosition(moveVector, thrustOffset.position);
    }

    Vector2 GetRelativeInputDirection(Vector2 globalVector)
    {
        Utility.LineRel(gameObject, globalVector, Color.red);
        Utility.LineRel(gameObject, transform.TransformVector(globalVector), Color.red);
        return transform.TransformVector(globalVector);
    }

    Vector2 GetInputDirection()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
