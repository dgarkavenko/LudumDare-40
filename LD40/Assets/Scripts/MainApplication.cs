using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainApplication : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private Transform _quad;
    
    private void Awake()
    {
        _playerCharacter.Init(_quad.localScale.x / 2, _quad.localScale.y / 2, arg =>
        {
            _uiController.ShowInteractionButton(RectTransformUtility.WorldToScreenPoint(Camera.main, arg));
        }, 
        () =>
        {
            _uiController.HideInteractionButton();            
        });
    }
}