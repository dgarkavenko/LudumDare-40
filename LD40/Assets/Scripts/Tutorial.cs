using System;
using System.Collections;
using UnityEngine;

public class Tutorial : MainApplication
{
    [SerializeField] private UITutorial _tutorial;
    [SerializeField] private TutorialCatSpawn _tutorialCatSpawn;
    
    protected override void Awake()
    {
        base.Awake();

        StartCoroutine(Co_Wait(() =>
        {
            PlayerCharacter.GrabThePole();
        }));
    }

    private IEnumerator Co_Wait(Action callback)
    {
        yield return new WaitForSeconds(.2f);

        callback();
    }
}