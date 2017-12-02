using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour 
{
    [SerializeField] private View _view;
    
    protected virtual void Update()
    {
        _view.transform.LookAt(_view.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}