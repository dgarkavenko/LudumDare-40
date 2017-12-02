using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private InteractionButton _interactionButton;

    public void ShowInteractionButton(Vector3 position)
    {
        _interactionButton.Show(position);
    }

    public void HideInteractionButton()
    {
        _interactionButton.Hide();
    }
}