using System;
using UnityEngine;

public class PlayerCharacter : Character
{
    private Vector3 _destination;

    private float _xScale;
    private float _zScale;
    
    private Action<Vector3> _onInteractionEnter;
    private Action _onInteractionExit;
    
    private DrowningCharacter _victim;

    public void Init(float xScale, float zScale, Action<Vector3> onInteractionEnter, Action onInteractionExit)
    {
        _xScale = xScale;
        _zScale = zScale;

        _onInteractionEnter = onInteractionEnter;
        _onInteractionExit = onInteractionExit;
    }
    
    protected override void Update()
    {
        base.Update();
        
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Translate(x, 0, z);
        
        var clampedPosition = transform.localPosition;
        
        clampedPosition.x = Mathf.Clamp(transform.localPosition.x, -_xScale, _xScale);
        clampedPosition.z = Mathf.Clamp(transform.localPosition.z, -_zScale, _zScale);
        
        transform.localPosition = clampedPosition;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_victim != null)
            {
                _victim.PickKitty();
                LeaveKitty();
            }
        }
    }

    private void LeaveKitty()
    {
        _onInteractionExit();

        _victim = null;
    }

    private void OnTriggerEnter(Collider col)
    {
        _victim = col.gameObject.GetComponent<DrowningCharacter>();
        if (_victim != null)
            _onInteractionEnter(_victim.InteractionPoint.position);
    }
    
    private void OnTriggerExit(Collider col)
    {
        _victim = col.gameObject.GetComponent<DrowningCharacter>();
        if (_victim != null)
        {
            LeaveKitty();
        }
    }
}