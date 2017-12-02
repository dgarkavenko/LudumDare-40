using System.Collections;
using System.Collections.Generic;
using BezierSplineTools;
using UnityEngine;

public class Stream : MonoBehaviour
{
    public BezierSpline spline;
    public float Step = .05f;
    public GameObject StreamZone;

    [ContextMenu("GenerateStreamZones")]
    public void GenerateStreamZones()
    {
        for (float i = 0.001f; i <= 1; i+= Step)
        {
            var go = GameObject.Instantiate(StreamZone);
            go.transform.position = spline.GetPoint(i);

            //go.transform.position = spline.GetDirection(i);

            Vector3 direction = spline.GetDirection(i);

            direction.y = 0;

            go.transform.forward = spline.GetDirection(i);

            var stream = go.GetComponent<StreamZone>();
            stream.Direction = direction;
        }
        //Curve
    }


	// Update is called once per frame
	void Update () {
		
	}
}
