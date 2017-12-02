using UnityEngine;

public class CameraController : MonoBehaviour 
{   
	[SerializeField] private Transform _target;
	[SerializeField] private Vector3 _offset;
	
	[SerializeField] private float _distance = 10.0f;
	[SerializeField] private float _height = 5.0f;
	
	[SerializeField] private float _heightDamping = 2.0f;
	[SerializeField] private float _rotationDamping = 3.0f;

	private void LateUpdate () 
	{
		var wantedHeight = _target.position.y + _height;

		var currentRotationAngle = transform.eulerAngles.y;
		var currentHeight = transform.position.y;

		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

		transform.position = _target.position + _offset;
		transform.position -= Vector3.forward * _distance;

		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
		transform.LookAt(_target);
	}
}