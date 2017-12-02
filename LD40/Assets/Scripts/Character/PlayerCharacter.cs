using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private View _view;
    
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
    
    private void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Translate(x, 0, z);

//        if (Input.GetKey(KeyCode.Mouse0))
//        {
//            var playerPlane = new Plane(Vector3.up, transform.position);
//            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            var hitDistance = 0.0f;
//
//            if (playerPlane.Raycast(ray, out hitDistance))
//            {
//                _destination = ray.GetPoint(hitDistance);
//
//                _navMeshAgent.SetDestination(_destination);
//            }
//        }
        
        var clampedPosition = transform.position;
        
        clampedPosition.x = Mathf.Clamp(transform.position.x, -_xScale, _xScale);
        clampedPosition.z = Mathf.Clamp(transform.position.z, -_zScale, _zScale);
        
        transform.position = clampedPosition;
        
        _view.transform.LookAt(_view.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

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