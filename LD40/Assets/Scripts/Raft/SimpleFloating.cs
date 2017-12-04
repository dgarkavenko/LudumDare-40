using UnityEngine;

public class SimpleFloating : MonoBehaviour
{
    public bool IsActive = true;
    public GameObject Target;
    public float Power = 2;
    public float StreamLerp = 4;
    public Vector3 FloatDirection = new Vector3(0.5f, 0, 0.5f);
    [HideInInspector] public Vector3 StreamDirection = new Vector3(0, 0, 0);

    public System.Action<Collision, SimpleFloating, SimpleFloating> OnCollisionEnterAction;

    public virtual void OnCollisionEnter(Collision other)
    {
        var otherSimple = other.other.GetComponent<SimpleFloating>();
        Debug.Log("COLLISION "  + gameObject.name + " with " + otherSimple.gameObject.name);
        if (OnCollisionEnterAction != null)
            OnCollisionEnterAction(other, this, otherSimple);
    }

    public virtual void Update()
    {
        if (!IsActive)
            return;

        Collider[] colliders = Physics.OverlapBox(Target.transform.position, new Vector3(3, 3, 3), Target.transform.rotation, LayerMask.GetMask("Stream"));
        if (colliders.Length > 0)
        {
            StreamDirection = Vector3.zero;
            for (int i = 0; i < colliders.Length; i++)
            {
                var stream = colliders[i].gameObject.GetComponent<StreamZone>();
                if (stream != null)
                    StreamDirection += stream.Direction;
            }

            StreamDirection = StreamDirection.normalized;
            FloatDirection = Vector3.Lerp(FloatDirection, StreamDirection, Time.deltaTime * StreamLerp);
        }

        Target.transform.position += FloatDirection.normalized * Time.deltaTime * Power;
    }

}
