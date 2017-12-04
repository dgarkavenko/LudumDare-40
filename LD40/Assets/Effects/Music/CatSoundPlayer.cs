using UnityEngine;
using Random = UnityEngine.Random;

public class CatSoundPlayer : MonoBehaviour
{
    [Range(0, 1)] public float Volume = 0.5f; 
    public AudioClip[] CatSounds;
    private AudioSource _audioSource;
    

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        CatSoundEventSender.PlayCatSound += PlayCatSound;
    }

    private void PlayCatSound()
    {
       _audioSource.PlayOneShot(CatSounds[GetCatSoundNumber()], Volume);
    }

    private void OnDestroy()
    {
        CatSoundEventSender.PlayCatSound -= PlayCatSound;
    }

    private int GetCatSoundNumber()
    {
        int soundNumber = Random.Range(0, CatSounds.Length);
        return soundNumber;
    }

}
