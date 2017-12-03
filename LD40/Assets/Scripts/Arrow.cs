using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _pivot;

    private void FixedUpdate()
    {
        _pivot.transform.LookAt(_target);
    }
}