using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringRopeGenerator : MonoBehaviour
{
    public int segments;

    public Transform starPos;
    public Transform endPos;

    private GameObject startPoint;
    private List<GameObject> points = new List<GameObject>();
    private GameObject endPoint;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = new GameObject();
        points.Add(startPoint);
        
        Rigidbody rgbd = startPoint.AddComponent<Rigidbody>();
        rgbd.isKinematic = true;

        for (int i = 0; i<segments; i++)
        {
            GameObject point = new GameObject();
            point.transform.parent = startPoint.transform;
            
            point.AddComponent<Rigidbody>();
            SpringJoint spring = point.AddComponent<SpringJoint>();

            spring.connectedBody = points[i].GetComponent<Rigidbody>();

            points.Add(point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}