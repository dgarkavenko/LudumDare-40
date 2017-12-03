using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : FloatingController
{
	[SerializeField] private Transform _view;

	private float _steer;
	public float SteeringSpeed = 1;

	public Transform ViewTransform => _view;

	public override void LateUpdate()
	{
		base.LateUpdate();
		float steer = Input.GetKey(KeyCode.A) ? -1 :
			Input.GetKey(KeyCode.D) ? 1 : 0;

		_steer = Mathf.Lerp(_steer, steer, Time.deltaTime * SteeringSpeed);
		Model.SteeringDirection = transform.TransformDirection(new Vector3(_steer, 0, 0));
	}

	public System.Action OnDrowningCatCollision;

	override public void OnCollisionEnterAction(Collision arg1, FloatingController arg2)
	{

		if (arg2 == null)
		{
			Debug.Log("Collision with static: " + arg1.impulse.magnitude + " impulse");
		}
		else if (arg2 is Obstacle)
		{
			Debug.Log("Collision with obstacle: " + arg1.impulse.magnitude + " impulse");

			if (arg1.impulse.magnitude > 5)
			{
				arg2.Model.rb.AddForce(Vector3.down * Model.rb.mass / arg2.Model.rb.mass, ForceMode.Impulse);
			}
		}
		else if (arg2 is DrowningCat && OnDrowningCatCollision != null)
		{
			OnDrowningCatCollision();
			Destroy(arg2.gameObject);
		}
		
	}
}
