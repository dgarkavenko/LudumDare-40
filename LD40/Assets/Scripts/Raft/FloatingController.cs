using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingController : MonoBehaviour
{
	public AQUAS_Buoyancy Model;
	public Vector3 Offset;

	public virtual void Start()
	{
		Model = SteeringBuoy.Instantiate(Model);
		Model.transform.position = transform.position;
		Model.transform.rotation = transform.rotation;
		Model.OnCollisionEnterAction += OnCollisionEnterAction;

	}

	public virtual void OnCollisionEnterAction(Collision obj)
	{
		throw new System.NotImplementedException();
	}

	public virtual void LateUpdate()
	{
		transform.SetPositionAndRotation(Model.transform.position + Model.transform.TransformDirection(Offset), Model.transform.rotation);
	}
}
