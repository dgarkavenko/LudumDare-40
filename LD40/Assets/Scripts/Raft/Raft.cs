using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : Floating {

	// Use this for initialization
	void Start () {
		
	}

	public Vector3 SteeringDirection = new Vector3();

    public float Speed;
	// Update is called once per frame
	void Update ()
	{

	  	base.Update();
		
		Debug.DrawRay(transform.position, SteeringDirection * 10, Color.red, Time.deltaTime);

		SteeringDirection = transform.TransformDirection(new Vector3(_steer / 2, 0, 0));
		
	    if (StreamDirection.magnitude > 0)
	    {
	        Quaternion streamDirection = Quaternion.LookRotation(FloatDirection + SteeringDirection, Vector3.up);

	        transform.rotation = Quaternion.Lerp(transform.rotation, streamDirection, Time.deltaTime);
		    
	    }

		Steer();
        transform.position += (FloatDirection  + SteeringDirection) * Time.deltaTime * Speed;
		Wiggle();
	}


	private float _steer;
	public float SteeringSpeed = 1;
	
	public void Steer()
	{
		float steer = Input.GetKey(KeyCode.A) ? -1 :
			Input.GetKey(KeyCode.D) ? 1 : 0;

		_steer = Mathf.Lerp(_steer, steer, Time.deltaTime * SteeringSpeed);

	}
}
