using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainApplication : MonoBehaviour
{
    [SerializeField] private UIController _uiController;

    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private Raft _raft;
    [SerializeField] private Stream _stream;

    public PlayerCharacter PlayerCharacter { get; private set; }

    private void Awake()
    {
        PlayerCharacter = Instantiate(_playerCharacter, _raft.transform);
        PlayerCharacter.Init(_raft.ViewTransform.localScale.x / 2, _raft.ViewTransform .localScale.z / 2, () =>
            {
                _uiController.ShowInteractionButton();
            },
            () =>
            {
                _uiController.HideInteractionButton();
            });

        _uiController.Init(PlayerCharacter.PickUpPoint);
        _stream.GenerateStreamZones();
        _stream.GenerateBanks();

    }

    public void PickCat(Cat cat)
    {
        PlayerCharacter.CatPicked(cat);
    }
}