using UnityEngine;

public class CatKeyController : MonoBehaviour
{
	[SerializeField] private Cat _cat;

	private void Update()
	{
		if (_cat == null)
			return;

		var x = Input.GetAxis("Horizontal") * 0.1f;
		var y = Input.GetAxis("Vertical") * 0.1f;

		if (x > Mathf.Epsilon || y > Mathf.Epsilon) {
			_cat.Move(new Vector2(x, y));
		}
	}
}
