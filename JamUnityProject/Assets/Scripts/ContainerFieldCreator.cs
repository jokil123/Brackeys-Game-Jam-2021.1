using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerFieldCreator : MonoBehaviour
{
    [SerializeField]
    private Vector3 size;

    private Vector3 objectSize;

    [SerializeField]
    private GameObject objectReference;

    private List<GameObject>[] volume;



    // Update is called once per frame
    void Update()
    {
        
    }
}
