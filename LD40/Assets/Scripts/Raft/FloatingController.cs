using UnityEngine;

public class FloatingController : MonoBehaviour
{
	[SerializeField] private AQUAS_Buoyancy _modelPrefab;
	public AQUAS_Buoyancy Model;
	public Vector3 Offset;

	public virtual void Start()
	{
		Model = SteeringBuoy.Instantiate(_modelPrefab);
		Model.transform.position = transform.position;
		Model.transform.rotation = transform.rotation;
		Model.OnCollisionEnterAction += OnCollisionEnterAction;
		Model.Controller = this;
		Model.waterLevel = Stream.WATER_LEVEL;
	}

	public virtual void OnCollisionEnterAction(Collision arg1, FloatingController arg2)
	{

	}

	private void OnDisable()
	{
		if(Model != null)
			Model.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		if (Model != null)
		{
			Model.transform.position = transform.position;
			Model.transform.rotation = transform.rotation;
			Model.gameObject.SetActive(true);
		}
	}

	public virtual void LateUpdate()
	{
		transform.SetPositionAndRotation(Model.transform.position + Model.transform.TransformDirection(Offset), Model.transform.rotation);
	}

	private void OnDestroy()
	{
		if (Model != null) {
			Model.OnCollisionEnterAction -= OnCollisionEnterAction;
			Destroy(Model);
		}
	}
}
