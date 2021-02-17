using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    GameObject segment;
    Transform start;
    Transform end;

    float width;

    public RopeSegment(Transform ropeStart, Transform ropeEnd, float ropeWidth)
    {
        start = ropeStart;
        end = ropeEnd;
        width = ropeWidth;

        segment = GenerateCylinder();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        UpdateCylinder();
    }

    private GameObject GenerateCylinder()
    {
        GameObject cylinderObject;
        GameObject cylinderModel;

        cylinderObject = new GameObject();
        cylinderModel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Destroy(cylinderObject.GetComponent<Collider>());

        cylinderModel.transform.parent = cylinderObject.transform;
        //cylinderObject.transform.parent = gameObject.transform;

        cylinderModel.transform.Rotate(new Vector3(0, 0, 90));

        Debug.Log("create");

        return cylinderObject;
    }

    private void UpdateCylinder()
    {
        segment.transform.position = CalculatePosition(start.position, end.position);
        segment.transform.GetChild(0).transform.localScale = CalculateScale(start.position, end.position, width);
        segment.transform.rotation = CalculateRotation(start.position, end.position);
    }

    private Vector3 CalculatePosition(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 position;

        position = (startPosition + endPosition) / 2;

        Debug.Log("Pos"); 
        return position;
    }

    private Vector3 CalculateScale(Vector3 startPosition, Vector3 endPosition, float width)
    {
        Vector3 scale;

        scale.x = width;
        scale.z = width;

        scale.y = (endPosition - startPosition).magnitude / 2;

        Debug.Log("Scale");
        return scale;
    }

    private Quaternion CalculateRotation(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 direction = (startPosition - endPosition).normalized;

        direction = Quaternion.AngleAxis(90, Vector3.up) * direction;

        Quaternion rotation = Quaternion.LookRotation(direction);

        Debug.Log("Rot");
        return rotation;
    }
}
