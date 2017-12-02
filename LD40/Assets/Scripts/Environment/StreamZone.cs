using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamZone : MonoBehaviour {

    public Vector3 Direction;

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Direction * 10);
    }

}
