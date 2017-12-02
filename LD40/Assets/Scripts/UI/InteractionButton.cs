using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionButton : MonoBehaviour 
{
    public void Show(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}