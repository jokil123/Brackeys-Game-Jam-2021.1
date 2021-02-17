using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringRopeGenerator : MonoBehaviour
{
    public int segments;

    public Transform starPos;
    public Transform endPos;

    public float ropeWidth;
    public Material ropeMaterial;

    public float segmentDrag;

    public float springS;
    public float damperS;
    public float minDistanceS;
    public float maxDistanceS;

    List<GameObject> ropePoints = new List<GameObject>();

    private void Start()
    {
        ropePoints.AddRange(GeneratePoints(segments, starPos, endPos));
        AddComponents(ropePoints);
    }

    private void Update()
    {

        for (int i = 0; i < segments - 1; i++)
        {
            Debug.DrawLine(ropePoints[i].transform.position, ropePoints[i + 1].transform.position, Color.red, Time.deltaTime, false);
        }
    }

    private void AddComponents(List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Rigidbody rgbd;
            rgbd = objects[i].AddComponent<Rigidbody>();
            rgbd.drag = segmentDrag;
        }

        objects[0].GetComponent<Rigidbody>().isKinematic = true;
        objects[0].transform.parent = starPos;

        objects[objects.Count - 1].GetComponent<Rigidbody>().isKinematic = true;
        objects[objects.Count - 1].transform.parent = endPos;

        for (int i = 0; i < objects.Count - 1; i++)
        {
            GameObject item = objects[i];
            SpringJoint spring = item.AddComponent<SpringJoint>();
            spring.connectedBody = objects[i + 1].GetComponent<Rigidbody>();
            spring.spring = springS;
            spring.damper = damperS;
            spring.minDistance = minDistanceS;
            spring.maxDistance = maxDistanceS;

            RopeSegment segmentModel = item.AddComponent<RopeSegment>();
            segmentModel.start = item.transform;
            segmentModel.end = objects[i + 1].transform;
            segmentModel.width = ropeWidth;
            segmentModel.parent = gameObject;
            segmentModel.material = ropeMaterial;
            segmentModel.CustomStart();
        }
    }

    private List<GameObject> GeneratePoints(int ammount, Transform start, Transform end)
    {
        GameObject parent = gameObject;

        List<GameObject> points = new List<GameObject>();

        for (int i = 0; i < ammount; i++)
        {
            GameObject point = new GameObject();

            point.transform.position = GetPointInbetween((float)i / (ammount - 1), start, end);

            point.transform.parent = parent.transform;

            points.Add(point);
        }

        return points;
    }

    private Vector3 GetPointInbetween(float progress, Transform start, Transform end)
    {
        Vector3 startEndLine = end.position - start.position;

        Vector3 pointPosition = startEndLine * progress + start.position;

        Debug.DrawLine(start.position, pointPosition, Color.white, Time.deltaTime, false);
        return pointPosition;
    }
}