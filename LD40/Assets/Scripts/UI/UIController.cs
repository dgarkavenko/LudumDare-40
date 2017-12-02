using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private InteractionButton _interactionButton;

    public void Init(Transform point)
    {
        _interactionButton.Init(point);
    }
    
    public void ShowInteractionButton()
    {
        _interactionButton.Show();
    }

    public void HideInteractionButton()
    {
        _interactionButton.Hide();
    }
}