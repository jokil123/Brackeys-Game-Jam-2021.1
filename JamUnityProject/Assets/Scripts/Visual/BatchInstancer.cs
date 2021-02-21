using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BatchInstancer : MonoBehaviour
{
    public List<Mesh> modelPieceMeshes = new List<Mesh>();
    public List<Material> modelPieceMaterials = new List<Material>();

    public void Instance(List<Matrix4x4> instancePSR)
    {
        BatchInstance(SplitIntoArrays(instancePSR));
    }


    void BatchInstance(List<Matrix4x4[]> batches)
    {
        foreach (Matrix4x4[] item in batches)
        {
            for (int i = 0; i < modelPieceMeshes.Count; i++)
            {
                Graphics.DrawMeshInstanced(modelPieceMeshes[i], 0, modelPieceMaterials[i], item);
            }
        }
    }

    List<Matrix4x4[]> SplitIntoArrays(List<Matrix4x4> inputList)
    {
        int maxArrLength = 1023;

        int neededArrays = Mathf.CeilToInt((float)inputList.Count / (float)maxArrLength);

        List<Matrix4x4[]> outputList = new List<Matrix4x4[]>();

        for (int i = 0; i < neededArrays; i++)
        {
            outputList.Add(new Matrix4x4[maxArrLength]);
        }

        for (int i = 0; i < inputList.Count; i++)
        {
            outputList[Mathf.CeilToInt(i / (maxArrLength))][i % maxArrLength] = inputList[i];
        }

        return outputList;
    }
}
