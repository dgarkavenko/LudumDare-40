using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _distance  = 10;

	[SerializeField] private float _smoothSpeed;
	[SerializeField] private Vector3 _offset;

	private void Update ()
	{
		var desiredPosition = _target.position + _offset;
		var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);

		transform.position = smoothedPosition;
	    transform.LookAt(_target);
	}
}