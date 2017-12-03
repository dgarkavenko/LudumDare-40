using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBuoy : AQUAS_Buoyancy {

	// Update is called once per frame
	override public void FixedUpdate () {
		
		if (balanceFactor.x < 0.001f){balanceFactor.x = 0.001f;}
		if (balanceFactor.y < 0.001f){balanceFactor.y = 0.001f;}
		if (balanceFactor.z < 0.001f){balanceFactor.z = 0.001f;}

		AddForce();
		
		var SumDirection = FloatDirection * StreamPower + SteeringDirection * SteerPower;

		SumDirection = SumDirection.normalized * StreamPower;
		
		rb.AddForce(SumDirection);

		var f = transform.forward;
		var t = SumDirection.normalized;

		var cross = Vector3.Cross(t, f);
		Debug.DrawRay(rb.transform.position, f * 5, Color.green, Time.fixedDeltaTime);
		Debug.DrawRay(rb.transform.position, t * 5, Color.red, Time.fixedDeltaTime);
	    
		rb.AddRelativeTorque(new Vector3(0,-cross.y * Torque,0));
	}
}
