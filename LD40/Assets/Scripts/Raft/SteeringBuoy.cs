using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBuoy : AQUAS_Buoyancy {

	// Update is called once per frame
	public override void FixedUpdate () {
		
		if (balanceFactor.x < 0.001f){balanceFactor.x = 0.001f;}
		if (balanceFactor.y < 0.001f){balanceFactor.y = 0.001f;}
		if (balanceFactor.z < 0.001f){balanceFactor.z = 0.001f;}

		AddForce();


		var steer = SteeringDirection.x;
		
		Debug.DrawRay(rb.transform.position, FloatDirection * StreamPower / 2, Color.yellow, Time.fixedDeltaTime);

		
		var SumDirection = Quaternion.Euler(0, steer * SteerPower, 0) * FloatDirection * StreamPower;

		Debug.DrawRay(rb.transform.position, SumDirection / 2, Color.red, Time.fixedDeltaTime);
		
		
		rb.AddForce(SumDirection, ForceMode.Force);

		rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxVelocity * (1 - steer / 3));
		
		Debug.DrawRay(rb.transform.position, rb.velocity, Color.magenta, Time.fixedDeltaTime);
		
		var f = rb.transform.forward;
		var t = rb.velocity.normalized;
		var cross = Vector3.Cross(t, f);
		rb.AddRelativeTorque(new Vector3(0, -cross.y * Torque, 0));
	}
}
