using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMarker : MonoBehaviour
{
    public GameObject goal;
    public GameObject goalArrow;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        DrawArrow();
        //goalArrow.transform.position = new Vector3(0, 0, 0);
    }

    void DrawArrow()
    {
        Vector3 screenpos = Camera.main.WorldToScreenPoint(goal.transform.position);

        //Check if point is on or offscreen
        if (screenpos.z > 0 && screenpos.x >0 && screenpos.x<Screen.width && screenpos.y>0 && screenpos.y<Screen.height)
        {
            goalArrow.transform.position = new Vector3(Screen.width+100,Screen.height+100,0);
            Debug.Log("In screen");
        } else
        {
            Debug.Log("Off screen");

            //Check if its behind the camera, if so flip it
            if (screenpos.z<0)
            {
                screenpos *= -1;
            }

            Vector3 screenCenter = new Vector3(Screen.width, Screen.height,0)/2;
            screenpos -= screenCenter;

            float angle = Mathf.Atan2(screenpos.y, screenpos.x);
            angle -= 90 * Mathf.Deg2Rad;

            float cos = Mathf.Cos(angle);
            float sin = -Mathf.Sin(angle);

            screenpos = screenCenter + new Vector3(sin*150, cos*150, 0);

            float m = cos / sin;

            Vector3 screenBounds = screenCenter * 0.9f;

            if (cos > 0)
            {
                screenpos = new Vector3(screenBounds.y / m, screenBounds.y, 0);
            } else
            {
                screenpos = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);
            }

            if (screenpos.x>screenBounds.x)
            {
                screenpos = new Vector3(screenBounds.x, screenBounds.x * m, 0);
            } else if (screenpos.x<-screenBounds.x)
            {
                screenpos = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);
            }

            screenpos += screenCenter;

            goalArrow.transform.position = screenpos;
            goalArrow.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }
    }
}
