using System;
using UnityEngine;

public class Raft : FloatingController
{
	[SerializeField] private Transform _view;
	[SerializeField] private RaftStick _raftStick;

	private float _steer;
	public float SteeringSpeed = 1;

	public Transform ViewTransform => _view;
	public RaftStick RaftStick => _raftStick;

	public Action OnDrowningCatCollision;
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
		Model.SteeringDirection = transform.TransformDirection(new Vector3(_steer, 0, 0));
	}

	public void SetControlStatus(bool value)
	{
		_playerControl = value;
	}

	override public void OnCollisionEnterAction(Collision arg1, FloatingController arg2)
	{

		if (arg2 == null)
		{
			Debug.Log("Collision with static");
			Debug.Log(arg1.impulse.magnitude);

			for (int i = 0; i < arg1.contacts.Length; i++)
			{
				var p = arg1.contacts[i].point;
				Debug.Log(transform.InverseTransformPoint(p));
			}

		}
		else if (arg2 is DrowningCat && OnDrowningCatCollision != null)
		{
			OnDrowningCatCollision();
			Destroy(arg2.gameObject);
		}
	}
}
