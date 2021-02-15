using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    // And so begins the 2½D pain

    public static Vector3 V2toV3(Vector2 vector)
    {
        return new Vector3(vector.x, 0, vector.y);
    }

    public static Vector2 V3toV2(Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }

    public static void LineRel(GameObject transformGameObject, Vector3 vector, Color color)
    {
        Debug.DrawLine(transformGameObject.transform.position, transformGameObject.transform.position + vector, color, Time.deltaTime, false);
    }
}