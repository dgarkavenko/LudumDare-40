using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : FloatingController
{
	public Vector2 RandomTorque;

	public override void OnCollisionEnterAction(Collision arg1, FloatingController arg2)
	{
		//base.OnCollisionEnterAction(arg1, arg2);
	}
}
