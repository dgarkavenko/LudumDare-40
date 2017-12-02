using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatSpawner : MonoBehaviour
{
	[SerializeField] private Transform _raft;
	[SerializeField] private MainApplication _main;
	public Raft Raft;

	private void Start()
	{
		/*for (var i = 0; i < 3; i++)
		{
			var cat = Instantiate(Links.Instance.Cat, _raft.parent);
			cat._raft = _raft;
			var position = Random.insideUnitCircle * 2f;
			cat.transform.localPosition = new Vector3(position.x, 1.025f, position.y);
			_main.PickCat(cat);
		}*/

		Raft.OnDrowningCatCollision += OnDrowningCatCollision;
	}

	private void OnDrowningCatCollision()
	{
		var cat = Instantiate(Links.Instance.Cat, _raft.parent);
		cat._raft = _raft;
		var position = Random.insideUnitCircle * 2f;
		cat.transform.localPosition = new Vector3(position.x, 1.025f, position.y);
		_main.PickCat(cat);
	}
}
