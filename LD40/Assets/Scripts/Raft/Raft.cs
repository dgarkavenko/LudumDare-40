using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : MonoBehaviour {

	public SteeringBuoy Model;
	public Vector3 Offset;
    public float Speed;
	// Update is called once per frame

	void Start()
	{
		Model = SteeringBuoy.Instantiate(Model);
		Model.transform.position = transform.position;
		Model.transform.rotation = transform.rotation;
		
	}
	
	void LateUpdate ()
	{
		transform.SetPositionAndRotation(Model.transform.position + Model.transform.TransformDirection(Offset), Model.transform.rotation);
		float steer = Input.GetKey(KeyCode.A) ? -1 :
			Input.GetKey(KeyCode.D) ? 1 : 0;

		_steer = Mathf.Lerp(_steer, steer, Time.deltaTime * SteeringSpeed);
		Model.SteeringDirection = transform.TransformDirection(new Vector3(_steer, 0, 0));
	}


	private float _steer;
	public float SteeringSpeed = 1;
	
}
