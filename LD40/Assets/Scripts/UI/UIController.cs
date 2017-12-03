using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private InteractionButton _interactionButton;
    [SerializeField] private Text _catCounter;

    public void Init(Transform point)
    {
        _interactionButton.Init(point);
    }

    public void SetCatsCount(int catCount)
    {
        _catCounter.text = catCount.ToString();
    }

    public void StartInteraction(Action callback)
    {
        _interactionButton.StartInteraction(callback);
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