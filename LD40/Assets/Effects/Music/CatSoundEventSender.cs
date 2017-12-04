using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatSoundEventSender : MonoBehaviour
{
    public float FirstMinDelta = 5;
    public float FirstMaxDelta = 20;
  
    private float _currentDelta = 0;
    private float _lastTime = 0;

    public static Action PlayCatSound;

    private void Start()
    {
        _currentDelta = Random.Range(FirstMinDelta, FirstMaxDelta);
    }

    private void Update()
    {
        /*
        if (Time.time - _lastTime > _currentDelta)
        {
            if(PlayCatSound != null)
                PlayCatSound();

            _lastTime = Time.time;
            _currentDelta = Random.Range(MinDelta, MaxDelta);
            Debug.Log(gameObject.name);
        }*/
    }
}
