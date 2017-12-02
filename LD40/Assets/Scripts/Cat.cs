using System;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Transform _raft;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _walkingSprite;
    [SerializeField] private Sprite _hangingSprite;
    [SerializeField] private Sprite _draggedSprite;

    private Vector3 _hitNormal;
    public float SlopeLimit = 10f;
    public float SlideFriction = 0.3f;

    public Transform Waypoint;

    public float MaxSpeed = 0.01f;

    public enum CatState
    {
        Walking,
        Hanging,
        BeingDragged
    }

    public CatState State;

    private void Update()
    {
        if (Waypoint != null)
            Move(Waypoint.position - transform.position);
        else
            Move(Vector3.zero);
    }

    public void Move(Vector3 direction2D)
    {
        if (State != CatState.Walking)
            return;

        var clampedDirection = Vector2.ClampMagnitude(new Vector2(direction2D.x, direction2D.z), MaxSpeed);
        var direction3D = new Vector3(clampedDirection.x, -1, clampedDirection.y);

        var isGrounded = Vector3.Angle (Vector3.up, _hitNormal) <= SlopeLimit;

        if (!isGrounded) {
            direction3D.x += (1f - _hitNormal.y) * _hitNormal.x * (1f - SlideFriction);
            direction3D.z += (1f - _hitNormal.y) * _hitNormal.z * (1f - SlideFriction);
        }

        _characterController.Move(direction3D);

        if (Waypoint != null && Vector3.Distance(transform.position, Waypoint.position) < 0.2f)
            Waypoint = null;
    }

    public void PickKitty()
    {
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        if (Waypoint != null) {
            Gizmos.DrawCube(Waypoint.position, Vector3.one * 0.1f);
            Gizmos.DrawLine(transform.position, Waypoint.position);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _hitNormal = hit.normal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Edge>() == null)
            return;

        State = CatState.Hanging;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        _spriteRenderer.sprite = GetSprite();
    }

    private Sprite GetSprite()
    {
        switch (State) {
            case CatState.Walking:
                return _walkingSprite;
            case CatState.Hanging:
                return _hangingSprite;
            case CatState.BeingDragged:
                return _draggedSprite;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetWaypoint(Vector3 waypointPosition)
    {
        if (Waypoint == null) {
            Waypoint = new GameObject("Waypoint").transform;
            Waypoint.SetParent(_raft);
        }

        Waypoint.position = waypointPosition;
    }
}
