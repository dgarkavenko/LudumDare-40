using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SixWayMovement _sixWayMovement;
    [SerializeField] private Transform _pickUp;

    [SerializeField] private float _speed = 3;
    [SerializeField] private float _pickUpDistance = 3;

    private readonly List<Cat> _cats = new List<Cat>();
    private Action _interaction;
    private Cat _savedCat;

    public Transform PickUpPoint => _pickUp;
    private RaftStick _stick;

    private Action _onInteractionEnter;
    private Action _onInteractionExit;

    private Action<Action> _onInteraction;
    private Action<bool> _raftControl;

    private float _xScale;
    private float _zScale;

    private bool _facedUp;
    private bool _controlRaft;

    public void Init(RaftStick stick, float xScale, float zScale, Action onInteractionEnter, Action onInteractionExit, Action<Action> onInteraction, Action<bool> raftControl)
    {
        _stick = stick;

        _xScale = xScale - .5f;
        _zScale = zScale - .5f;

        _onInteractionEnter = onInteractionEnter;
        _onInteractionExit = () =>
        {
            _interaction = null;

            onInteractionExit();
        };

        _onInteraction = onInteraction;
        _raftControl = raftControl;
    }

    protected void LateUpdate()
    {
        if (_controlRaft) return;

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        if (z != 0)
            _facedUp = z > 0;

        _spriteRenderer.sprite = _sixWayMovement.GetSprite(_facedUp, x);

        var cam = Camera.main;

        transform.position += cam.transform.rotation * new Vector3(x, 0, z) * _speed * Time.deltaTime;

        var clampedPosition =
            new Vector3(transform.localPosition.x, 0, transform.localPosition.z)
            {
                x = Mathf.Clamp(transform.localPosition.x, -_xScale, _xScale),
                z = Mathf.Clamp(transform.localPosition.z, -_zScale, _zScale)
            };

        transform.localPosition = clampedPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_interaction != null)
            {
                _onInteraction(() =>
                {
                    _interaction();
                });
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            _onInteractionExit();
        }

        foreach (var kitty in _cats)
        {
            if (Vector3.Distance(transform.position, kitty.transform.position) <= _pickUpDistance)
            {
                SetSavedCat(kitty);
                return;
            }
        }

        _savedCat = null;

        if (Vector3.Distance(transform.position, _stick.transform.position) <= _pickUpDistance)
        {
            if (_interaction != null) return;

            _onInteractionEnter();

            if (!_controlRaft)
            {
                _interaction = () =>
                {
                    _controlRaft = true;
                    _raftControl(true);
                    _onInteractionExit();
                };
            }
            else
            {
                _interaction = () =>
                {
                    _controlRaft = false;
                    _raftControl(false);
                    _onInteractionExit();
                };
            }

            return;
        }

        _onInteractionExit();
        _controlRaft = false;
    }

    private void SetSavedCat(Cat cat)
    {
        if (_savedCat == cat) return;

        _savedCat = cat;

        _onInteractionEnter();

        var fight = (cat.State as Cat.Fighting)?.Fight;

        if (fight != null)
        {
            _interaction = () => fight.Stop();
        }
        else
        {
            _interaction = () =>
            {
                _cats.Remove(cat);
                cat.PickKitty();
            };
        }
    }

    public void CatPicked(Cat cat)
    {
        _cats.Add(cat);
    }
}