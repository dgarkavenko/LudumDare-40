using UnityEngine;

public class SimpleFloating : MonoBehaviour
{
    public float Power = 2;
    public float StreamLerp = 4;
    public Vector3 FloatDirection = new Vector3(0.5f, 0, 0.5f);
    [HideInInspector] public Vector3 StreamDirection = new Vector3(0, 0, 0);
    
    public void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(3, 3, 3), transform.rotation, LayerMask.GetMask("Stream"));
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

        transform.position += FloatDirection.normalized * Time.deltaTime * Power;
    }

}
