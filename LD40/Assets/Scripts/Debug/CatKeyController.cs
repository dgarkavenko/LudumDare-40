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

		var walkingCat = _cat.State as Cat.Walking;

		if (walkingCat != null) {
			if (x > Mathf.Epsilon || y > Mathf.Epsilon)
				walkingCat.Move(new Vector2(x, y));

			if (Input.GetKeyDown(KeyCode.Space)) {
				walkingCat.PossibleAttackTarget?.Attack();
				walkingCat.NearbyFight?.Join();
			}
		}
	}
}
