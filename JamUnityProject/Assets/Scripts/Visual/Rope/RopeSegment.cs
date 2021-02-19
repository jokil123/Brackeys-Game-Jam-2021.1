using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    GameObject segment;

    public GameObject parent;
    public Transform start;
    public Transform end;

    public float width;
    public Material material;

    void Update()
    {
        if (segment != null)
        {
            UpdateCylinder();
        }
    }

    public void CustomStart()
    {
        segment = GenerateCylinder();
    }

    public GameObject GenerateCylinder()
    {
        GameObject cylinderObject;
        GameObject cylinderModel;

        cylinderObject = new GameObject();
        cylinderModel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinderModel.GetComponent<MeshRenderer>().material = material;
        Destroy(cylinderModel.GetComponent<Collider>());

        cylinderModel.transform.parent = cylinderObject.transform;
        cylinderObject.transform.parent = parent.transform;

        cylinderModel.transform.Rotate(new Vector3(90, 0, 0));

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

        Quaternion rotation = Quaternion.LookRotation(direction);

        Debug.Log("Rot");
        return rotation;
    }
}
