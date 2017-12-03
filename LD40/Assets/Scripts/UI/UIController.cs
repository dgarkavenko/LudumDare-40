using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private InteractionButton _interactionButton;
    [SerializeField] private Text _catCounter;

    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _pauseScreen;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            SetPauseStatus(!_pauseScreen.activeSelf);
        });

        _restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Main");
        });

        _exitButton.onClick.AddListener(() =>
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        });
    }

    public void Init(Transform point)
    {
        SetPauseStatus(false);

        _interactionButton.Init(point);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPauseStatus(!_pauseScreen.activeSelf);
        }
    }

    private void SetPauseStatus(bool status)
    {
        if (status)
        {
            _pauseScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            _pauseScreen.SetActive(false);
            Time.timeScale = 1.0f;
        }
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