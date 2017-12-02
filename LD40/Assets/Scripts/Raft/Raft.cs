using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : FloatingController {

	// Update is called once per frame
	private float _steer;
	public float SteeringSpeed = 1;

	override public void LateUpdate()
	{
		base.LateUpdate();
		float steer = Input.GetKey(KeyCode.A) ? -1 :
			Input.GetKey(KeyCode.D) ? 1 : 0;

		_steer = Mathf.Lerp(_steer, steer, Time.deltaTime * SteeringSpeed);
		Model.SteeringDirection = transform.TransformDirection(new Vector3(_steer, 0, 0));
	}
}
