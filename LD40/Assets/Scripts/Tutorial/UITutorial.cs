using System;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] _steps;

    [SerializeField] private Button[] _buttons;

    private Action _callback;

    private void Awake()
    {
        foreach (var button in _buttons)
        {
            button.onClick.AddListener(() =>
            {
                Time.timeScale = 1f;

                _callback();
            });
        }
    }

    public void SetTutorial(int step, Action callback)
    {
        Time.timeScale = 0f;

        _callback = callback;

        foreach (var @obj in _steps)
        {
            @obj.SetActive(false);
        }

        _steps[step].SetActive(true);
    }

    public void Disable()
    {
        foreach (var @obj in _steps)
        {
            @obj.SetActive(false);
        }
    }
}