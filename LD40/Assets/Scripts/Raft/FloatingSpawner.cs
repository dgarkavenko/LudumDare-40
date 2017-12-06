using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSpawner : MonoBehaviour
{
	public SimpleFloating Floating;
	public SphereCollider SphereCollider;
	// Use this for initialization
	private void OnTriggerEnter(Collider other)
	{
		gameObject.layer = LayerMask.NameToLayer("Floating");
		Floating.enabled = true;
		Floating.StreamPower /= Random.Range(2, 3);
		enabled = false;
		SphereCollider.enabled = false;
	}
}
