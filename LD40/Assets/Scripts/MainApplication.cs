using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainApplication : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private Transform _raft;
    [SerializeField] private Stream _stream;

    
    private void Awake()
    {
        _playerCharacter.Init(_raft.localScale.x / 2, _raft.localScale.z / 2, arg =>
        {
            _uiController.ShowInteractionButton(RectTransformUtility.WorldToScreenPoint(Camera.main, arg));
        }, 
        () =>
        {
            _uiController.HideInteractionButton();            
        });
        
        
        _stream.GenerateStreamZones();
    }
}