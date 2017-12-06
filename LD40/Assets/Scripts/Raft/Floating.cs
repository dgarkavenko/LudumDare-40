using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {

	public Vector3 FloatDirection = new Vector3(0,0,1);
	private Vector3 _streamDirection = new Vector3(0,0,1);
	public Vector3 SteeringDirection;
	public float StreamPower = 6;
	public float SteerPower = 2;
	public float StreamLerp = 10;
	public float Torque = 1;
	public float MaxVelocity = 50;
	public float waterLevel;
	public float waterDensity;

	public Rigidbody rb;

	public FloatingController Controller { get; set; }

	public System.Action<Collision, FloatingController> OnCollisionEnterAction;
    
	private void OnCollisionEnter(Collision other)
	{
		var floating = other.gameObject.GetComponent<Floating>();

		if (OnCollisionEnterAction != null)
			OnCollisionEnterAction(other, floating == null ? null : floating.Controller);
	}

	public static float WaterLevel;

	public virtual void Update()
	{
		Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(3, 3, 3), transform.rotation, LayerMask.GetMask("Stream"));
		if (colliders.Length > 0)
		{
			_streamDirection = Vector3.zero;
			for (int i = 0; i < colliders.Length; i++)
			{
				var stream = colliders[i].gameObject.GetComponent<StreamZone>();
				if(stream != null)
					_streamDirection += stream.Direction;
			}

			_streamDirection = _streamDirection.normalized;
			FloatDirection = Vector3.Lerp(FloatDirection, _streamDirection, Time.deltaTime * StreamLerp);
		}
	}

	public virtual void Drown()
	{
		
	}
}
