using System;
using System.Collections;
using System.Timers;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform _target;
	private Raft _raft;

	[Serializable]
	public class CamerSetting
	{
		public float Distance = 10;
		public float SideShift = 3;
		public float Angle = 45;
		public float CameraPosSmoothTime;
		public float CameraRotSmoothTime;
		public Vector3 CameraPrediction = new Vector3(0,0,2f);

	}

	public void Init()
	{
		SetSettings(CloseupCamera, 2);
		_raft = _target.gameObject.GetComponent<Raft>();
		_mag = SteeringCamera.CameraPrediction.magnitude;
	}

	private float _mag = 1;
	
	private void FixedUpdate ()
	{
		
		//SteeringCamera.CameraPrediction = _target.TransformDirection(_raft.Model.StreamDirection).normalized * _mag;
		SteeringMode();


		/*var wantedHeight = _target.position.y + _height;

		var currentRotationAngle = transform.eulerAngles.y;
		var currentHeight = transform.position.y;

		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

		transform.position = _target.position + _offset;
		transform.position -= Vector3.forward * _distance;

		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
		transform.LookAt(_target);*/
	}

	public void SetSettings(CamerSetting settings, float time)
	{
		StopAllCoroutines();
		StartCoroutine(Swithc(time, settings));

		Debug.Log("Change Camera");
	}

	IEnumerator Swithc(float time, CamerSetting settings)
	{
		float passed = 0;

		while (passed < time)
		{
			passed += Time.deltaTime;
			var t = passed / time;

			Distance = Mathf.Lerp(Distance, settings.Distance, t);
			SideShift = Mathf.Lerp(SideShift, settings.SideShift, t);
			Angle = Mathf.Lerp(Angle, settings.Angle, t);
			CameraPosSmoothTime = Mathf.Lerp(CameraPosSmoothTime, settings.CameraPosSmoothTime, t);
			CameraRotSmoothTime = Mathf.Lerp(CameraRotSmoothTime, settings.CameraRotSmoothTime, t);
			CameraPrediction = Vector3.Lerp(CameraPrediction, settings.CameraPrediction, t);

			yield return null;
		}

		Distance = settings.Distance;
		SideShift = settings.SideShift;
		Angle = settings.Angle;
		CameraPosSmoothTime = settings.CameraPosSmoothTime;
		CameraRotSmoothTime = settings.CameraRotSmoothTime;
		CameraPrediction = settings.CameraPrediction;

		yield break;
	}

	[SerializeField]
	public CamerSetting SteeringCamera;
	[SerializeField]
	public CamerSetting CloseupCamera;

	private Vector3 _velocity;
	private Vector3 _velocityRotation;

	private Vector3 _lookTarget;
	public float Distance = 10;
	public float SideShift = 3;
	public float Angle = 45;
	public float CameraPosSmoothTime;
	public float CameraRotSmoothTime;
	public Vector3 CameraPrediction = new Vector3(0,0,2f);


	private void SteeringMode()
	{
		var backwards = new Vector3(-_target.forward.x, 0, -_target.forward.z).normalized;

		backwards = Vector3.Lerp(backwards, new Vector3(-1,0,-1).normalized, 0.8f);
		
		//var backwards = new Vector3(0, 0, -1);
		
		
		var crossBackwards = new Vector3(backwards.z, 0, -backwards.x);

		var horizontal = backwards * Mathf.Cos(Angle * Mathf.Deg2Rad) * Distance;
		var vertical = new Vector3(0, 1, 0) * Mathf.Sin(Angle * Mathf.Deg2Rad) * Distance;
		var t = _target.position + Quaternion.Euler(0, SideShift, 0) * (horizontal + vertical);
		//transform.position = t;

		//transform.position = Vector3.SmoothDamp(transform.position, t, ref _velocity, CameraPosSmoothTime);

		
		transform.position = Vector3.Lerp(transform.position, t, Time.deltaTime * CameraPosSmoothTime);
		_lookTarget = Vector3.Lerp(_lookTarget, _target.position + _raft.Model.FloatDirection * CameraPrediction.magnitude,
			Time.deltaTime * CameraRotSmoothTime);

		transform.LookAt(_lookTarget);

	}

	public void SetControlStatus(bool value)
	{
		SetSettings(value ? SteeringCamera : CloseupCamera, 2);
	}
}