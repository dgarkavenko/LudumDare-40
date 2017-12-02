using UnityEngine;

public class CatMouseController : MonoBehaviour
{
	[SerializeField] private Cat _controlledCat;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, LayerMask.GetMask("Raft"))) {
				_controlledCat.SetWaypoint(hit.point);
			}
		}
	}
}
