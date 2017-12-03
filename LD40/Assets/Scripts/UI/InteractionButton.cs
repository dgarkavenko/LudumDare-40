using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private Image _filler;

    private Transform _point;

    private Coroutine _interaction;
    private float _appearSpeed = 6.0f;

    public void Init(Transform point)
    {
        _point = point;
        _filler.fillAmount = 0;
    }

    private void FixedUpdate()
    {
        if (_point == null) return;

        transform.position = Vector3.Lerp(transform.position, RectTransformUtility.WorldToScreenPoint(Camera.main, _point.position), 5f);
    }

    public void StartInteraction(Action callback)
    {
        if (!_view.activeSelf)
        {
            Debug.LogError("Are you nuts?");
            return;
        }

        if (_interaction != null)
        {
            StopCoroutine(_interaction);
            _interaction = null;
        }

        _interaction = StartCoroutine(Co_Wait(1, callback));
    }

    private IEnumerator Co_Wait(float time, Action callback)
    {
        var t = 0f;

        while (t < time)
        {
            _filler.fillAmount = t;

            t += Time.deltaTime;

            yield return null;
        }

        callback();
    }

    private IEnumerator Co_Show()
    {
        _view.SetActive(true);
        _view.transform.localScale = Vector3.zero;

        while (_view.transform.localScale.x < 1.2)
        {
            _view.transform.localScale = new Vector3(_view.transform.localScale.x + Time.deltaTime * _appearSpeed, _view.transform.localScale.y + Time.deltaTime * _appearSpeed,_view.transform.localScale.z + Time.deltaTime * _appearSpeed);
            yield return null;
        }

        _view.transform.localScale = Vector3.one;
    }

    public void Show()
    {
        StartCoroutine(Co_Show());
    }

    public void Hide()
    {
        if (_interaction != null)
        {
            StopCoroutine(_interaction);
            _interaction = null;
        }

        _filler.fillAmount = 0;

        _view.SetActive(false);
    }
}