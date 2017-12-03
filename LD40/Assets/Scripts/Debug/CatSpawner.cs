using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatSpawner : MonoBehaviour
{
	[SerializeField] private Transform _raft;
	[SerializeField] private MainApplication _main;
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
	};

	private int _catNumber;

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

		for (var i = 0; i < 3; i++)
			SpawnCat();

		Raft.OnDrowningCatCollision += OnDrowningCatCollision;
	}

	private void SpawnCat()
	{
		var cat = Instantiate(Links.Instance.Cats.PickRandom(), _raft.parent);
		cat._raft = _raft;
		var position = Random.insideUnitCircle * 2f;
		cat.transform.localPosition = new Vector3(position.x, Cat.RaftSurfaceY, position.y);
		var brain = cat.gameObject.AddComponent<CatBrain>();
		brain.Cat = cat;

		_catNumber++;

		if (_catNames.Count > 0) {
			var nameIndex = Random.Range(0, _catNames.Count);
			cat.Name = _catNames[nameIndex];
			_catNames.RemoveAt(nameIndex);
		} else {
			cat.Name = $"Cat {_catNumber}";
		}

		cat.State = new Cat.Walking(cat);
		cat.MainApplication = _main;
		_main.PickCat(cat);
	}

	private void OnDrowningCatCollision(Cat cat, Vector3 pos)
	{
		cat.State = new Cat.Hanging();
		cat.transform.parent = _raft.parent;
		_main.PickCat(cat);
	}
}
