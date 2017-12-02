using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform Target;
    public float Distance  = 10;
    public float Angle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position = Target.position + new Vector3(0, 1, -1) * Distance;
	    transform.LookAt(Target.position + Target.forward * 5);
	}
}
