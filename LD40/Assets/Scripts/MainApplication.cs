using UnityEngine;

public class MainApplication : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private UIController _uiController;

    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private Raft _raft;
    [SerializeField] private Stream _stream;

    private PlayerCharacter PlayerCharacter { get; set; }
    private int _catCount;

    private void Awake()
    {
        PlayerCharacter = Instantiate(_playerCharacter, _raft.transform);
        PlayerCharacter.Init(_raft.RaftStick, _raft.ViewTransform.localScale.x / 2, _raft.ViewTransform .localScale.z / 2, () =>
            {
                _uiController.ShowInteractionButton();
            },
            () =>
            {
                _uiController.HideInteractionButton();
            },
            (time, arg) =>
            {
                _uiController.StartInteraction(time, arg);
            },
            arg =>
            {
                _cameraController.SetControlStatus(arg);
                _raft.SetControlStatus(arg);
            });

        _cameraController.Init();
        _uiController.Init(PlayerCharacter.PickUpPoint);
        _stream.GenerateStreamZones();
        _stream.GenerateBanks();

    }

    public void PickCat(Cat cat)
    {
        _catCount++;

        PlayerCharacter.CatPicked(cat);

        _uiController.SetCatsCount(_catCount);
    }
}