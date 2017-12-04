using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestNoise : MonoBehaviour
{
    public AudioClip _noiseClip;
    [Range(0, 1)] public float _noiseVolume;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_noiseClip, _noiseVolume);
    }
}