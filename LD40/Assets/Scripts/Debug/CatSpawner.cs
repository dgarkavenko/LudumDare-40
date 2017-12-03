using UnityEngine;
using Random = UnityEngine.Random;

public class CatSpawner : MonoBehaviour
{
	[SerializeField] private Transform _raft;
	[SerializeField] private MainApplication _main;
	public Raft Raft;

	private void Start()
	{
		for (var i = 0; i < 3; i++)
		{
			var cat = Instantiate(Links.Instance.Cats.PickRandom(), _raft.parent);
			cat._raft = _raft;

			var position = Random.insideUnitCircle * 2f;

			cat.transform.localPosition = new Vector3(position.x, 1.025f, position.y);
			cat.State = new Cat.Walking(cat);

			_main.PickCat(cat);
		}

		Raft.OnDrowningCatCollision += OnDrowningCatCollision;
	}

	private void OnDrowningCatCollision(Vector3 point)
	{
		var cat = Instantiate(Links.Instance.Cats.PickRandom(), _raft.parent);
		cat._raft = _raft;
		//var position = Random.insideUnitCircle * 2f;
		//cat.transform.localPosition = new Vector3(position.x, 1.025f, position.y);

		cat.transform.position = new Vector3(point.x, point.y + 1.5f, point.z);
		cat.State = new Cat.Hanging();

		_main.PickCat(cat);
	}
}
