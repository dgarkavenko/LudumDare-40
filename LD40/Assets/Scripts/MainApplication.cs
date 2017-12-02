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
        _uiController.Init(_playerCharacter.PickUpPoint);
        
        _playerCharacter.Init(_raft.localScale.x / 2, _raft.localScale.z / 2, () =>
            {
                _uiController.ShowInteractionButton();
            }, 
            () =>
            {
                _uiController.HideInteractionButton();            
            });
        
        _stream.GenerateStreamZones();
    }

    public void PickCat(Cat cat)
    {
        _playerCharacter.CatPicked(cat);
    }
}