using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowningCharacter : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    
    public Transform InteractionPoint
    {
        get { return _interactionPoint; }
    }

    public void PickKitty()
    {
        Destroy(gameObject);
    }
}