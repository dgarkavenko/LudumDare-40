﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {

	public Vector3 FloatDirection = new Vector3(0,0,1);
	public Vector3 StreamDirection = new Vector3(0,0,1);
	public float StreamLerp = 10;
	public float FloatSpeed = 4;
	
	
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

	public AnimationCurve waveX;
	public AnimationCurve waveY;
	public AnimationCurve positionCurve;

	public void Wiggle()
	{
		float t = transform.position.z / 4;
		
		transform.eulerAngles = new Vector3(waveX.Evaluate(t), transform.eulerAngles.y, waveY.Evaluate(t));
		transform.position = new Vector3(transform.position.x, positionCurve.Evaluate(t), transform.position.z);
	}
	
}
