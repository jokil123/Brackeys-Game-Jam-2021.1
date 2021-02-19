using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum Direction
{
    Up,
    Down,
    Right,
    Left,
    Forward,
    Backward
}


public class BoundaryBox : MonoBehaviour
{
    private BoxCollider boxCollider;

    List<GameObject> boundaryChildren = new List<GameObject>();

    List<GameObject> allCollisionObjects = new List<GameObject>();


    private void UpdateList()
    {
        allCollisionObjects.Clear();
        foreach (GameObject item in boundaryChildren)
        {
            List<GameObject> list = new List<GameObject>(item.GetComponent<BoundaryChild>().collisionObjects);
            allCollisionObjects.AddRange(list);
        }
    }


    public bool IsColliding(GameObject collisionObject)
    {
        UpdateList();

        foreach (GameObject item in allCollisionObjects)
        {
            if (item == collisionObject)
            {
                return true;
            }
        }

        return false;
    }


    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        GenerateChildren();
    }


    private void GenerateChildren()
    {
        foreach (GameObject item in boundaryChildren)
        {
            Destroy(item);
        }

        boundaryChildren.Add(GenerateBoxColliderChild(Direction.Up));
        boundaryChildren.Add(GenerateBoxColliderChild(Direction.Down));
        boundaryChildren.Add(GenerateBoxColliderChild(Direction.Right));
        boundaryChildren.Add(GenerateBoxColliderChild(Direction.Left));
        boundaryChildren.Add(GenerateBoxColliderChild(Direction.Forward));
        boundaryChildren.Add(GenerateBoxColliderChild(Direction.Backward));
    }

    private GameObject GenerateBoxColliderChild(Direction colliderDirection)
    {
        GameObject child;
        BoxCollider col;

        child = new GameObject();
        child.transform.position = gameObject.transform.position;
        child.transform.rotation = gameObject.transform.rotation;
        child.transform.localScale = gameObject.transform.localScale;
        child.transform.parent = gameObject.transform;

        child.AddComponent<BoundaryChild>();


        col = child.AddComponent<BoxCollider>();
        col.isTrigger = true;
        col.size = boxCollider.size;
        col.center = boxCollider.center;

        Vector3 cent = col.center;

        switch (colliderDirection)
        {
            case Direction.Up:
                cent.y += col.size.y;
                break;
            case Direction.Down:
                cent.y -= col.size.y;
                break;
            case Direction.Right:
                cent.x += col.size.x;
                break;
            case Direction.Left:
                cent.x -= col.size.x;
                break;
            case Direction.Forward:
                cent.z += col.size.z;
                break;
            case Direction.Backward:
                cent.z -= col.size.z;
                break;
        }

        col.center = cent;

        return child;
    }
}