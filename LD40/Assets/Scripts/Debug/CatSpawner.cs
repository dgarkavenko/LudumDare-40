using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatSpawner : MonoBehaviour
{
	[SerializeField] private Transform _raft;
	[SerializeField] private MainApplication _main;
    [SerializeField] private int _catsCount = 0;
	public Raft Raft;

	private readonly List<string> _catNames = new List<string> {
		"Ashes",
		"Molly",
		"Charlie",
		"Tigger",
		"Poppy",
		"Oscar",
		"Chester",
		"Millie",
		"Daisy",
		"Max",
		"Jasper",
		"Trevor",
		"Mr. Whiskers"
	};

	public int CatNumber;

	private void Shuffle(List<string> texts)
	{
		for (int t = 0; t < texts.Count; t++ )
		{
			string tmp = texts[t];
			int r = Random.Range(t, texts.Count);
			texts[t] = texts[r];
			texts[r] = tmp;
		}
	}

	private void Start()
	{
		Shuffle(_catNames);

		for (var i = 0; i < _catsCount; i++) {
			var position = Random.insideUnitCircle * 2f;
			SpawnCat(new Vector3(position.x, Cat.RaftSurfaceY, position.y));
		}

		Raft.OnDrowningCatCollision += OnDrowningCatCollision;
	}

	public void SpawnCat(Vector3 position, bool drowning = false, Action onPicked = null, float hangTime = 0)
	{
		var cat = Instantiate(Links.Instance.Cats.PickRandom());

		cat.transform.SetParent(drowning ? _raft.parent.parent : _raft.parent);

		if (drowning)
			cat.transform.position = position;
		else
			cat.transform.localPosition = position;

		var brain = cat.gameObject.AddComponent<CatBrain>();
		brain.Cat = cat;

		CatNumber++;

		if (_catNames.Count > 0) {
			var nameIndex = Random.Range(0, _catNames.Count);
			cat.Name = _catNames[nameIndex];
			_catNames.RemoveAt(nameIndex);
		} else {
			cat.Name = $"Cat {CatNumber}";
		}

		cat.State = drowning ? (Cat.CatState)new Cat.Drowning(cat) : new Cat.Walking(cat);
        cat.Init(_main, Raft, _raft);
		_main._uiController.CreateCatUi(cat);

		if (!drowning)
			_main.PickCat(cat);

	    cat.OnPickedOnce = onPicked;

	    if (hangTime > 0)
	        cat.HangingTime = hangTime;
	}

	private void OnDrowningCatCollision(Cat cat, Vector3 pos)
	{
	    if (cat.DrownedForGood)
	        return;

		cat.State = new Cat.Hanging(cat);
		cat.transform.parent = _raft.parent;

        cat.transform.localPosition = new Vector3(cat.transform.localPosition.x, .55f, cat.transform.localPosition.z);

        _main.PickCat(cat);
	}
}
