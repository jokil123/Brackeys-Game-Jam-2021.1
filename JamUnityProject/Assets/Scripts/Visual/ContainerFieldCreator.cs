using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LoopMethods
{
    UpdateField,
    GenerateField,
    DeleteRandom,
    MakeHollow
}

public class ContainerFieldCreator : MonoBehaviour
{
    [SerializeField]
    private Vector3Int size;

    [SerializeField]
    private Vector3 objectSize;

    [SerializeField]
    private float layerMultiplier;

    [SerializeField]
    private GameObject objectReference;

    private GameObject[,,] fieldObjects;



    private void Awake()
    {
        fieldObjects = new GameObject[size.x, size.y, size.z];

        LoopDaLoop(LoopMethods.GenerateField);
        LoopDaLoop(LoopMethods.DeleteRandom);
        LoopDaLoop(LoopMethods.MakeHollow);
    }

    void DeleteFromField(Vector3Int positionIndex)
    {
        for (int iY = positionIndex.y; iY < size.y; iY++)
        {
            Destroy(fieldObjects[positionIndex.x, iY, positionIndex.z]);
            fieldObjects[positionIndex.x, iY, positionIndex.z] = null;
        }
    }

    void DeleteRandom(Vector3Int index)
    {
        float threshold = Mathf.Pow(1f / ((float)index.y + 1), size.y * layerMultiplier);

        if (Random.Range(0f, 1f) > threshold)
        {
            DeleteFromField(index);
        }
    }


    void LoopDaLoop(LoopMethods method)
    {
        for (int iX = 0; iX < size.x; iX++)
        {
            for (int iY = 0; iY < size.y; iY++)
            {
                for (int iZ = 0; iZ < size.z; iZ++)
                {
                    Vector3Int index = new Vector3Int(iX, iY, iZ);

                    switch (method)
                    {
                        case LoopMethods.UpdateField:
                            if (fieldObjects[iX, iY, iZ] != null)
                            {
                                UpdateField(index);
                            }
                            break;
                        case LoopMethods.GenerateField:
                            GenerateField(index);
                            break;
                        case LoopMethods.DeleteRandom:
                            if (fieldObjects[iX, iY, iZ] != null)
                            {
                                DeleteRandom(index);
                            }
                            break;
                        case LoopMethods.MakeHollow:
                            bool canMakeHollow = true;

                            bool[] containerDirections = new bool[6];

                            containerDirections[0] = IsThereAContainer(index + Vector3Int.up);
                            containerDirections[1] = IsThereAContainer(index + Vector3Int.down);
                            containerDirections[2] = IsThereAContainer(index + Vector3Int.right);
                            containerDirections[3] = IsThereAContainer(index + Vector3Int.left);
                            containerDirections[4] = IsThereAContainer(index + Vector3Int.forward);
                            containerDirections[5] = IsThereAContainer(index + Vector3Int.back);

                            foreach (bool item in containerDirections)
                            {
                                if (!item)
                                {
                                    canMakeHollow = false;
                                }
                            }

                            bool IsThereAContainer(Vector3Int positionIndex)
                            {
                                if (positionIndex.x <= size.x - 1 && positionIndex.y <= size.y - 1 && positionIndex.z <= size.z - 1)
                                {
                                    if (positionIndex.x >= 0 && positionIndex.y >= 0 && positionIndex.z >= 0)
                                    {
                                        if (fieldObjects[positionIndex.x, positionIndex.y, positionIndex.z] != null)
                                        {
                                            return true;
                                        }
                                    }
                                }
                                return false;
                            }


                            if (canMakeHollow)
                            {
                                Destroy(fieldObjects[index.x, index.y, index.z]);
                            }

                            break;
                    }
                }
            }
        }
    }

    void UpdateField(Vector3Int index)
    {
        fieldObjects[index.x, index.y, index.z].transform.position = GetObjectPosition(index) + gameObject.transform.position;
    }

    void GenerateField(Vector3Int index)
    {
        fieldObjects[index.x, index.y, index.z] = GenerateObject(GetObjectPosition(index));
    }




    GameObject GenerateObject(Vector3 position)
    {
        GameObject instance = Instantiate(objectReference);

        instance.transform.parent = gameObject.transform;

        Vector3 pos = gameObject.transform.position + position;
        pos += objectSize / 2;

        instance.transform.position = pos;

        return instance;
    }

    Vector3 GetObjectPosition(Vector3 fieldPosition)
    {
        //Vector3 startingPoint = gameObject.transform.position;

        float x = objectSize.x * fieldPosition.x;
        float y = objectSize.y * fieldPosition.y;
        float z = objectSize.z * fieldPosition.z;

        Vector3 position = new Vector3(x, y, z);

        return position;
    }

    private void OnDrawGizmos()
    {
        Vector3 boxSize = GetBoxSize();

        Vector3 boxPosition;

        boxPosition = boxSize / 2 + gameObject.transform.position;

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(boxPosition, boxSize);
    }

    public Vector3 GetBoxSize()
    {
        Vector3 boxSize;

        boxSize.x = size.x * objectSize.x;
        boxSize.y = size.y * objectSize.y;
        boxSize.z = size.z * objectSize.z;

        return boxSize;
    }
}
