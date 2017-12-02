using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public AQUAS_Buoyancy Model;
	public Vector3 Offset;
	public Vector2 RandomTorque;

	private float _torque;
	
	void Start()
	{
		Model = AQUAS_Buoyancy.Instantiate(Model);
		Model.transform.SetPositionAndRotation(transform.position, transform.rotation);
		Model.StreamPower /= Random.Range(0.9f, 2.5f);
		//_torque = Random.Range(RandomTorque.x, RandomTorque.y);
		

	}

	private void Update()
	{
		transform.SetPositionAndRotation(Model.transform.position + Model.transform.TransformDirection(Offset), Model.transform.rotation);
	
	}
}
