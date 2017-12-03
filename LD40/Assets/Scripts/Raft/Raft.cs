using System;
using UnityEngine;

public class Raft : FloatingController
{
	[SerializeField] private Transform _view;
	[SerializeField] private RaftStick _raftStick;
	[SerializeField] private Transform _stickPivot;
	[SerializeField] private Transform _mastPivot;

	private float _health = 100;

	private float _steer;
	public float SteeringSpeed = 1;

	public Transform ViewTransform => _view;
	public RaftStick RaftStick => _raftStick;

	public Action<Cat, Vector3> OnDrowningCatCollision;
	private bool _playerControl;

	public override void LateUpdate()
	{
		base.LateUpdate();

		float steer = 0;

		if (_playerControl)
		{
			steer = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
		}

		_steer = Mathf.Lerp(_steer, steer, Time.deltaTime * SteeringSpeed);

		_stickPivot.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(_steer + transform.rotation.y * 100, -30, 50));
		_mastPivot.transform.localRotation = Quaternion.Euler(0, 0, -Mathf.Clamp(_steer + transform.rotation.y * 100, -30, 50));

		Model.SteeringDirection = transform.TransformDirection(new Vector3(_steer, 0, 0));
	}

	public void SetControlStatus(bool value)
	{
		_playerControl = value;
	}

	public override void OnCollisionEnterAction(Collision arg1, FloatingController arg2)
	{
		if (arg2 == null || arg2 is Obstacle)
		{
			_health -= arg1.impulse.magnitude / 10;
		}

		if (arg2 == null)
		{
			Debug.Log("Collision with static: " + arg1.impulse.magnitude + " impulse");
		}
		else if (arg2 is Obstacle)
		{
			Debug.Log("Collision with obstacle: " + arg1.impulse.magnitude + " impulse");

			if (arg1.impulse.magnitude > 5)
			{
				arg2.Model.rb.AddForce(Vector3.down * Model.rb.mass / arg2.Model.rb.mass, ForceMode.Impulse);
			}
		}
		else if (arg2 is DrowningCat)
		{
			OnDrowningCatCollision?.Invoke(arg2.GetComponent<Cat>(), arg1.contacts[0].point);
		}
	}
}
