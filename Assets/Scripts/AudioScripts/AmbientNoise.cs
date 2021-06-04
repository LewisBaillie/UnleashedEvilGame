using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbientNoise : MonoBehaviour
{
    [SerializeField]
    private AudioSource amb1;

    [SerializeField]
    private List<AudioClip> _ambAudioClips;

    private AudioSource selectedAudioSource;
    private AudioClip selectedAudioClip;

    private float counter;
    private float previousCounter;

    private void Awake()
    {
        amb1.clip = null;
        selectedAudioSource = null;
    }

    private void Start()
    {
        counter = 5f;
        previousCounter = counter;
        selectedAudioSource = amb1;
    }

    private void Update()
    {
        counter -= Time.deltaTime;

        if(counter <= 0f && !selectedAudioSource.isPlaying)
        {
            selectedAudioClip = _ambAudioClips[Random.Range(0, 3)];

            selectedAudioSource.clip = selectedAudioClip;

            selectedAudioSource.Play();

            StartCoroutine(waitTime());
        }
    }

    private IEnumerator waitTime()
    {
        yield return new WaitForSeconds(2.0f);
        selectedAudioSource.clip = null;
        if (previousCounter < 6)
        {
            counter = Random.Range(3, 6);
        }
        else
        {
            counter = Random.Range(6, 9);
        }
        previousCounter = counter;
    }
}
