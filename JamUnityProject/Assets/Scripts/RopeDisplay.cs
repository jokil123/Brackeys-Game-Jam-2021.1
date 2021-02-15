using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDisplay : MonoBehaviour
{
    private List<SpringJoint> springs = new List<SpringJoint>();

    private void Start()
    {
        springs.AddRange(gameObject.GetComponents<SpringJoint>());
    }

    void Update()
    {
        foreach (SpringJoint i in springs)
        {
            Vector3 springStart = i.anchor;
            Vector3 springEnd = i.connectedAnchor;

            Vector3 lineStart = transform.TransformVector(springStart) + gameObject.transform.position;
            Vector3 lineEnd = i.connectedBody.transform.TransformVector(springEnd) + i.connectedBody.transform.position;

            Debug.DrawLine(lineStart, lineEnd, Color.blue, Time.deltaTime, false);
        }
    }
}
