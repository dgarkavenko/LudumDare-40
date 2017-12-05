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

		for (var i = 0; i < 5; i++) {
			var position = Random.insideUnitCircle * 2f;
			SpawnCat(new Vector3(position.x, Cat.RaftSurfaceY, position.y));
		}

		Raft.OnDrowningCatCollision += OnDrowningCatCollision;
	}

	public void SpawnCat(Vector3 position, bool drowning = false)
	{
		var cat = Instantiate(Links.Instance.Cats.PickRandom());
		cat._raft = _raft;

		cat.transform.SetParent(drowning ? _raft.parent.parent : _raft.parent);

		if (drowning)
			cat.transform.position = position;
		else
			cat.transform.localPosition = position;

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

		cat.State = drowning ? (Cat.CatState)new Cat.Drowning(cat) : new Cat.Walking(cat);
		cat.MainApplication = _main;
		_main._uiController.CreateCatUi(cat);

		if (!drowning)
			_main.PickCat(cat);
	}

	private void OnDrowningCatCollision(Cat cat, Vector3 pos)
	{
	    if (cat.DrownedForGood)
	        return;

		cat.State = new Cat.Hanging(cat);
		cat.transform.parent = _raft.parent;
		_main.PickCat(cat);
	}
}
