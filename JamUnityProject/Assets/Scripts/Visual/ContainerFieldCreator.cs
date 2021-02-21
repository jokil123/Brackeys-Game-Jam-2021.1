using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerFieldCreator : MonoBehaviour
{
    public Vector3Int size;

    public Vector3 objectSize;

    public float layerMultiplier;



    private bool[,,] fieldWeight;

    private List<Matrix4x4> matricies = new List<Matrix4x4>();


    private void Start()
    {
        fieldWeight = new bool[size.x, size.y, size.z];

        LoopIt(LoopMethods.TrueField);
        LoopIt(LoopMethods.DisableRandom);
        LoopIt(LoopMethods.MakeHollow);
        matricies = GeneratePositionMatricies(fieldWeight);
    }

    private void Update()
    {
        gameObject.GetComponent<BatchInstancer>().Instance(matricies);
    }


    List<Matrix4x4> GeneratePositionMatricies(bool[,,] field)
    {
        List<Matrix4x4> outputList = new List<Matrix4x4>();

        for (int iX = 0; iX < field.GetLength(0); iX++)
        {
            for (int iY = 0; iY < field.GetLength(1); iY++)
            {
                for (int iZ = 0; iZ < field.GetLength(2); iZ++)
                {
                    if (fieldWeight[iX, iY, iZ])
                    {
                        float posX = objectSize.x * iX + objectSize.x / 2;
                        float posY = objectSize.y * iY + objectSize.y / 2;
                        float posZ = objectSize.z * iZ + objectSize.z / 2;

                        Vector3 localPosition = new Vector3(posX, posY, posZ);

                        Vector3 rotatedLocalPosition = gameObject.transform.rotation * localPosition;

                        Vector3 position = gameObject.transform.position + rotatedLocalPosition;

                        Quaternion rotation = gameObject.transform.rotation;

                        Vector3 scale = gameObject.transform.localScale;


                        Matrix4x4 objectTRS = Matrix4x4.TRS(position, rotation, scale);

                        outputList.Add(objectTRS);
                    }
                }
            }
        }

        return outputList;
    }


    void TrueField(Vector3Int position)
    {
        fieldWeight[position.x, position.y, position.z] = true;
    }


    void DisableRandom(Vector3Int position)
    {
        float threshold = Mathf.Pow(1f / ((float)position.y + 1), position.y * layerMultiplier);

        if (Random.Range(0f, 1f) > threshold)
        {
            DeleteFromField(position);
        }
    }


    void DeleteFromField(Vector3Int position)
    {
        for (int iY = position.y; iY < size.y; iY++)
        {
            fieldWeight[position.x, iY, position.z] = false;
        }
    }


    void MakeHollow(Vector3Int position)
    {
        bool canMakeHollow = true;

        bool[] containerDirections = new bool[6];

        containerDirections[0] = IsThereAContainer(position + Vector3Int.up);
        containerDirections[1] = IsThereAContainer(position + Vector3Int.down);
        containerDirections[2] = IsThereAContainer(position + Vector3Int.right);
        containerDirections[3] = IsThereAContainer(position + Vector3Int.left);
        containerDirections[4] = IsThereAContainer(position + Vector3Int.forward);
        containerDirections[5] = IsThereAContainer(position + Vector3Int.back);

        foreach (bool item in containerDirections)
        {
            if (!item)
            {
                canMakeHollow = false;
            }
        }

        if (canMakeHollow)
        {
            fieldWeight[position.x, position.y, position.z] = false; ;
        }
    }

    bool IsThereAContainer(Vector3Int position)
    {
        if (position.x <= size.x - 1 && position.y <= size.y - 1 && position.z <= size.z - 1)
        {
            if (position.x >= 0 && position.y >= 0 && position.z >= 0)
            {
                if (fieldWeight[position.x, position.y, position.z])
                {
                    return true;
                }
            }
        }
        return false;
    }



    void LoopIt(LoopMethods method)
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
                        case LoopMethods.TrueField:
                            TrueField(index);
                            break;
                        case LoopMethods.DisableRandom:
                            DisableRandom(index);
                            break;
                        case LoopMethods.MakeHollow:
                            MakeHollow(index);
                            break;
                    }
                }
            }
        }
    }



    private void OnDrawGizmos()
    {
        Vector3 boxSize = GetBoxSize();

        Vector3 boxPosition;

        boxPosition = boxSize / 2;

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.lossyScale);
        Gizmos.matrix = rotationMatrix;

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


public enum LoopMethods
{
    TrueField,
    DisableRandom,
    MakeHollow
}