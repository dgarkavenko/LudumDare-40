using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class Raft : FloatingController
{
    [SerializeField] private MainApplication _main;
	[SerializeField] private Transform _view;
	[SerializeField] private RaftStick _raftStick;
	[SerializeField] private Transform _stickPivot;
	[SerializeField] private Transform _mastPivot;
	
	[SerializeField] private List<Transform> _frontParts;
	[SerializeField] private List<Transform> _leftParts;
	[SerializeField] private List<Transform> _rightParts;
	[SerializeField] private Transform[] _logs;
	
	private float _health = 50;

	private float _steer;
	public float SteeringSpeed = 1;

	public Transform ViewTransform => _view;
	public RaftStick RaftStick => _raftStick;

	public SpriteRenderer tutorial;

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

		
		var f = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
		var a = Vector3.Angle(f, Model.FloatDirection);

		var cross = Vector3.Cross(f, Model.FloatDirection);

		var sign = Math.Sign(cross.y);
		
		_mastPivot.transform.localRotation = Quaternion.Euler(0,0, a * sign);

			
		Model.SteeringDirection = new Vector3(_steer, 0, 0);

		if (Vector3.Dot(Model.transform.up, Vector3.up) < 0.2f)
		{
			LowerHealth(5);
			foreach (var l in _logs)
			{
				var r = l.localRotation.eulerAngles + Random.insideUnitSphere * 6;
				r = Vector3.ClampMagnitude(r, 15);
				l.localRotation = Quaternion.Euler(r);
			}
		}
			
		
	}

	public void SetControlStatus(bool value)
	{
		_playerControl = value;
	}


	private float _accumulatedDamage;
	bool isDead = false;
	public void LowerHealth(float d, float cross = 0)
	{
		_health -= d;
		_accumulatedDamage += d;
		
		if (_health < 0 && !isDead)
		{
			_main._uiController.Lost();

			isDead = true;
			StartCoroutine(Drown());
			foreach (var p in _leftParts.Concat(_frontParts.Concat(_rightParts)))
			{
				UnAttach(p);
			}
			
			return;
		}
		
		if (_accumulatedDamage > 8)
		{
			_accumulatedDamage = 0;
			
			var a = cross < -.2f ? _rightParts :
				cross > .2f ? _leftParts : _frontParts;

			
			if(a.Count < 1)
				return;
			
			var o = a[Random.Range(0, a.Count)];

			a.Remove(o);
			UnAttach(o);
		}

		
	}

	public void UnAttach(Transform o)
	{
		o.parent = null;
		o.gameObject.layer = LayerMask.NameToLayer("Хуйня");
		o.gameObject.AddComponent<BoxCollider>();
		var r = o.gameObject.GetComponent<Rigidbody>();
		if(r == null)
			r = o.gameObject.AddComponent<Rigidbody>();
		r.AddForce(Vector3.up * 50, ForceMode.Acceleration);
		r.mass = 2;
		r.drag = 0.3f;

		var b = o.gameObject.AddComponent<AQUAS_Buoyancy>();
		b.waterDensity = 4;
		b.waterLevel = 1;
		b.StreamPower = 2;
	}

	IEnumerator Drown()
	{
		var c = Camera.main.GetComponent<CameraController>();
		c.enabled = false;
		
		while (Model.waterDensity > 0)
		{
			Model.waterDensity -= Time.deltaTime * 3.5f;
			yield return null;
		}
	}

	public event Action GreatCollisionAction; 
	
	public override void OnCollisionEnterAction(Collision arg1, FloatingController arg2)
	{
		
		if (arg2 is DrowningCat)
		{
			OnDrowningCatCollision?.Invoke(arg2.GetComponent<Cat>(), arg1.contacts[0].point);
			return;
		}
		
		if (arg1.impulse.magnitude < 10)
			return;

		
		float damage = 0;
		
		if (arg2 == null)
		{
			damage = arg1.impulse.magnitude / 20f;
		}
		else if (arg2 is Obstacle)
		{
			damage = arg1.impulse.magnitude / 2f;
			arg2.Model.rb.AddForce(Vector3.down * Model.rb.mass / arg2.Model.rb.mass, ForceMode.Impulse);
		}
		
		var impulse = arg1.impulse.normalized;
		var f = new Vector3(transform.forward.x, 0, transform.forward.z);
		var i = new Vector3(impulse.x, 0, impulse.z);
		var cross = Vector3.Cross(f, i);

		if (Mathf.Abs(cross.y) > .4f)
			damage *= .8f;
		
		Debug.Log("DAMAGE: " + damage + " health: " + _health);
		
		if (damage > 8)
		{
			if (GreatCollisionAction != null)
				GreatCollisionAction();
			
			foreach (var l in _logs)
			{
				var r = l.localRotation.eulerAngles + Random.insideUnitSphere * damage / 3;
				r = Vector3.ClampMagnitude(r, 15);
				l.localRotation = Quaternion.Euler(r);
			}
		}
		
		LowerHealth(damage, cross.y);

		/*var count = _parts.childCount;
		var part = _parts.GetChild(Random.Range(0, count));
		part.parent = null;
		part.gameObject.layer = LayerMask.NameToLayer("Default");
		
		var rb = part.gameObject.AddComponent<Rigidbody>();
		rb.mass = 5;
		rb.AddForce(Vector3.up * 500 + arg1.impulse * 20, ForceMode.Force);
		rb.AddTorque(Random.onUnitSphere * 5);
		
		var f = part.gameObject.AddComponent<AQUAS_Buoyancy>();
		f.waterDensity = 5;
		f.waterLevel = 1;
		f.StreamPower = 1;*/
	}
}
