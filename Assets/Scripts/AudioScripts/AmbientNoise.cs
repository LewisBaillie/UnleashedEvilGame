using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbientNoise : MonoBehaviour
{
    [SerializeField]
    private AudioSource amb1;
    [SerializeField]
    private AudioSource amb2;
    [SerializeField]
    private AudioSource amb3;

    [SerializeField]
    private List<AudioClip> _ambAudioClips;

    private AudioSource selectedAudioSource;
    private AudioClip selectedAudioClip;

    private float counter;
    private float previousCounter;

    private void Awake()
    {
        amb1.clip = null;
        amb2.clip = null;
        amb3.clip = null;
        selectedAudioSource = null;
    }

    private void Start()
    {
        counter = 5f;
        previousCounter = counter;
    }

    private void Update()
    {
        counter -= Time.deltaTime;

        if(counter <= 0f)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    selectedAudioSource = amb1;
                    break;
                case 1:
                    selectedAudioSource = amb2;
                    break;
                case 2:
                    selectedAudioSource = amb3;
                    break;
            }

            selectedAudioClip = _ambAudioClips[Random.Range(0, 3)];

            selectedAudioSource.clip = selectedAudioClip;

            selectedAudioSource.Play();

            if(!selectedAudioSource.isPlaying)
            {
                selectedAudioSource.clip = null;
                if(previousCounter < 6)
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
    }
}
