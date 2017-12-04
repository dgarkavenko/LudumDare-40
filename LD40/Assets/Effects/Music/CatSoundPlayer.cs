using UnityEngine;
using Random = UnityEngine.Random;

public class CatSoundPlayer : MonoBehaviour
{
    [Range(0, 1)] public float Volume = 0.5f; 
    public AudioClip[] CatSounds;
    private AudioSource _audioSource;

    public float MinDelta = 5;
    public float MaxDelta = 20;

    private float _currentDelta = 0;
    private float _lastTime = 0;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentDelta = Random.Range(MinDelta, MaxDelta);
    }

    private void Update()
    {
        if (Time.time - _lastTime > _currentDelta)
        {
            _audioSource.PlayOneShot(CatSounds[GetCatSoundNumber()], Volume);
            _lastTime = Time.time;
            _currentDelta = Random.Range(MinDelta, MaxDelta);
        }
    }
    
    private int GetCatSoundNumber()
    {
        int soundNumber = Random.Range(0, CatSounds.Length);
        return soundNumber;
    }

}
