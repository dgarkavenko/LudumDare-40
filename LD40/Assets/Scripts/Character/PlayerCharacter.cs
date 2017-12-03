using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SixWayMovement _sixWayMovement;
    [SerializeField] private Transform _pickUp;

    [SerializeField] private float _speed = 3;
    [SerializeField] private float _pickUpDistance = 3;

    private readonly List<Cat> _cats = new List<Cat>();

    public Transform PickUpPoint => _pickUp;

    private Action _onInteractionEnter;
    private Action _onInteractionExit;
    private Action<Action> _onInteraction;

    private Cat _interactionCat;

    private float _xScale;
    private float _zScale;

    private bool _facedUp;

    public void Init(float xScale, float zScale, Action onInteractionEnter, Action onInteractionExit, Action<Action> onInteraction)
    {
        _xScale = xScale - .5f;
        _zScale = zScale - .5f;

        _onInteractionEnter = onInteractionEnter;
        _onInteractionExit = onInteractionExit;
        _onInteraction = onInteraction;
    }

    protected void LateUpdate()
    {
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
            if (_interactionCat != null)
            {
                Debug.Log("has cat");
                var fight = (_interactionCat.State as Cat.Fighting)?.Fight;

                if (fight != null)
                {
                    fight.Stop();
                }
                else
                {
                    _onInteraction(() =>
                    {
                        _cats.Remove(_interactionCat);
                        _interactionCat.PickKitty();

                        SetSavedCat(null);
                    });
                }
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

        SetSavedCat(null);
    }

    private void SetSavedCat(Cat cat)
    {
        if (cat != null)
        {
            _onInteractionEnter();
        }
        else
        {
            _onInteractionExit();
        }

        _interactionCat = cat;
    }

    public void CatPicked(Cat cat)
    {
        _cats.Add(cat);
    }

//    private void OnTriggerEnter(Collider col)
//    {
//        _victim = col.gameObject.GetComponent<DrowningCharacter>();
//        if (_victim != null)
//        {
//            Debug.Log("Enter");
//        }
//    }
//
//    private void OnTriggerExit(Collider col)
//    {
//        if (col.gameObject.GetComponent<DrowningCharacter>() != null)
//        {
//            Debug.Log("Exit");
//
//            LeaveKitty();
//        }
//    }

}