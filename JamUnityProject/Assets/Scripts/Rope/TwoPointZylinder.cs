using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPointZylinder : MonoBehaviour
{
    
    public float ropeWidth;

    public Transform start;
    public Transform end;

    private GameObject rope;

    private void Start()
    {
        rope = GenerateCylinder();
    }


    private void Update()
    {
        UpdateCylinder(rope, start.position, end.position, ropeWidth);
    }



    private Vector3 CalculatePosition(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 position;

        position = (startPosition + endPosition) / 2;

        return position;
    }

    private Vector3 CalculateScale(Vector3 startPosition, Vector3 endPosition, float width)
    {
        Vector3 scale;

        scale.x = width;
        scale.z = width;

        scale.y = (endPosition - startPosition).magnitude / 2;

        return scale;
    }

    private Quaternion CalculateRotation(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 direction = (startPosition - endPosition).normalized;

        direction = Quaternion.AngleAxis(90, Vector3.up) * direction;

        Quaternion rotation = Quaternion.LookRotation(direction);

        return rotation;
    }

    private GameObject GenerateCylinder()
    {
        GameObject cylinderObject;
        GameObject cylinderModel;

        cylinderObject = new GameObject();
        cylinderModel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Destroy(cylinderObject.GetComponent<Collider>());

        cylinderModel.transform.parent = cylinderObject.transform;
        cylinderObject.transform.parent = gameObject.transform;
        
        cylinderModel.transform.Rotate(new Vector3(0, 0, 90));
        return cylinderObject;
    }

    private void UpdateCylinder(GameObject cylinderObject, Vector3 startPosition, Vector3 endPosition, float width)
    {
        cylinderObject.transform.position = CalculatePosition(startPosition, endPosition);
        cylinderObject.transform.GetChild(0).transform.localScale = CalculateScale(startPosition, endPosition, width);
        cylinderObject.transform.rotation = CalculateRotation(startPosition, endPosition);
    }
}
