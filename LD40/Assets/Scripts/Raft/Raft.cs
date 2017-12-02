using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


    public Vector3 FloatDirection = new Vector3(0,0,1);
    public Vector3 StreamDirection = new Vector3(0,0,1);
    public float Speed;
	// Update is called once per frame
	void Update ()
	{

	    //Vector3 dir = FloatDirection;

	    Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(3, 3, 3), transform.rotation, LayerMask.GetMask("Stream"));
	    if (colliders.Length > 0)
	    {
	        StreamDirection = Vector3.zero;
	        for (int i = 0; i < colliders.Length; i++)
	        {
	            var stream = colliders[i].gameObject.GetComponent<StreamZone>();
                if(stream != null)
                    StreamDirection += stream.Direction;
	        }

	        StreamDirection = StreamDirection.normalized;
	        FloatDirection = Vector3.Lerp(FloatDirection, StreamDirection, Time.deltaTime * 10);
	    }

	    if (StreamDirection.magnitude > 0)
	    {
	        Quaternion streamDirection = Quaternion.LookRotation(StreamDirection, Vector3.up);

	        transform.rotation = Quaternion.Lerp(transform.rotation, streamDirection, Time.deltaTime);
	    }

        transform.position += FloatDirection * Time.deltaTime * Speed;
        //

    }
}
