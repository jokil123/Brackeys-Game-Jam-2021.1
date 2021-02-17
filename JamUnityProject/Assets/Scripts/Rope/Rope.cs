using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject testStart;
    public GameObject testEnd;

    // Start is called before the first frame update
    void Start()
    {
        RopeSegment segment = new RopeSegment(testStart.transform, testEnd.transform, 1);
    }
}
