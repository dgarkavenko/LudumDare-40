using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionButton : MonoBehaviour 
{
    private Transform _point;

    public void Init(Transform point)
    {
        _point = point;
    }

    private void Update()
    {
        if (_point == null) return;

        transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _point.position);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}