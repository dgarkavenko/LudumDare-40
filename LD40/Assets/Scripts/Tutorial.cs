using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MainApplication
{
    [SerializeField] private UITutorial _tutorial;
    [SerializeField] private TutorialCatSpawn _tutorialCatSpawn;
    
    protected override void Awake()
    {
        base.Awake();

        _tutorialCatSpawn.OnCatPicked = () =>
        {
            _tutorial.SetTutorial(2, () =>
            {
                (PlayerCharacter as TutorialPlayerCharacter).OnPoleGrab = () =>
                {
                    _tutorial.SetTutorial(3, () =>
                    {
                        _tutorial.Disable();

                        StartCoroutine(Co_Wait(() =>
                        {
                            _tutorial.SetTutorial(4, () =>
                            {
                                PlayerPrefs.SetInt("BledNavalny", 1);

                                SceneManager.LoadScene("Main", LoadSceneMode.Single);
                                SceneManager.LoadScene("Level", LoadSceneMode.Additive);
                            });
                        }));
                    });
                };

                _tutorial.Disable();
            });
        };

        SceneManager.LoadScene("Level", LoadSceneMode.Additive);

        _tutorial.SetTutorial(0, () =>
        {
            _tutorial.SetTutorial(1, () =>
            {
                _tutorial.Disable();
            });
        });
    }

    private IEnumerator Co_Wait(Action callback)
    {
        yield return new WaitForSeconds(8);

        callback();
    }
}