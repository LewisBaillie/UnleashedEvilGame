using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> m_playerAudioClips;

    [SerializeField]
    private AudioSource m_PlayerAudioSource;

    public enum SoundEffect
    {
        nullSound,
        pickupSound,
        doorSound,
        torchSound,
        throwingSound,
    }

    public void playPlayerAudio(SoundEffect soundIndex)
    {
        m_PlayerAudioSource.clip = m_playerAudioClips[(int)soundIndex - 1];
        m_PlayerAudioSource.Play();
        if(!m_PlayerAudioSource.isPlaying)
        {
            m_PlayerAudioSource.clip = null;
        }
    }
}
