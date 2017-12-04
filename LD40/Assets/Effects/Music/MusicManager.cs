using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] Tracks;
    public float FadeTime = 1.0f;
    private int _currentTrack = 0;
    private bool _isSwitched = false;
    private AudioSource _currentAudioSource;
	
	private void Awake ()
	{
	    Crossfade(Tracks[GetTrackNumber()]);
	}
	
	private void Update ()
    {
        if (_currentAudioSource != null && _currentAudioSource.clip != null && _currentAudioSource.clip.length - _currentAudioSource.time < 30 && !_isSwitched)
        {
            _isSwitched = true;
            Crossfade(Tracks[GetTrackNumber()]);
        }
	}

    public void Crossfade(AudioClip newTrack)
	{
		StopAllCoroutines();

		if(GetComponents<AudioSource>().Length > 1)
		{
			Destroy(GetComponent<AudioSource>());
		}

        _currentAudioSource = gameObject.AddComponent<AudioSource>();
        _currentAudioSource.volume = 0.0f;
        _currentAudioSource.clip = newTrack;
        _currentAudioSource.Play();
        StartCoroutine(ActuallyCrossfade(_currentAudioSource));
	}

	IEnumerator ActuallyCrossfade(AudioSource newSource)
	{
		float t = 0.0f;
        float initialVolume = GetComponent<AudioSource>().volume;

		while(t < FadeTime)
		{
			GetComponent<AudioSource>().volume = Mathf.Lerp(initialVolume,0.0f,t/FadeTime);
			newSource.volume = Mathf.Lerp(0.0f,1.0f,t/FadeTime);

			t += Time.deltaTime;
			yield return null;
		}

		newSource.volume = 1.0f;
	    _isSwitched = false;
        Destroy(GetComponent<AudioSource>());
	}

    private int GetTrackNumber()
    {
        var number = _currentTrack;
        if (_currentTrack >= Tracks.Length)
        {
            _currentTrack = 0;
            number = _currentTrack;
        }
        _currentTrack++;
        Debug.Log(number);
        return number;
    }
}
