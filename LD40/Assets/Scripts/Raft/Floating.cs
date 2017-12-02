using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {

	public Vector3 FloatDirection = new Vector3(0,0,1);
	public Vector3 StreamDirection = new Vector3(0,0,1);
	public Vector3 SteeringDirection;
	public float StreamPower = 6;
	public float SteerPower = 2;
	public float StreamLerp = 10;
	public float Torque = 1;

	public static float WaterLevel;
	
	public void Update()
	{
		Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(3, 3, 3), transform.rotation, LayerMask.GetMask("Stream"));
		if (colliders.Length > 0)
		{
			StreamDirection = Vector3.zero;
			for (int i = 0; i < colliders.Length; i++)
			{
				var stream = colliders[i].gameObject.GetComponent<StreamZone>();
				if(stream != null)
					StreamDirection += stream.Direction;
			}

			StreamDirection = StreamDirection.normalized;
			FloatDirection = Vector3.Lerp(FloatDirection, StreamDirection, Time.deltaTime * StreamLerp);
		}

	}
	
}
