using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffect
{
    nullSound,
    pickupSound,
    doorSound,
    torchSound,
    throwingSound,
}

public class Sound : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _AllAudioClips;
    
    public void PlaySound(SoundEffect soundIndex)
    {
        GameObject newObj = new GameObject("Audio");
        newObj.AddComponent<AudioSource>();
        newObj.GetComponent<AudioSource>().clip = _AllAudioClips[(int)soundIndex - 1];
        newObj.GetComponent<AudioSource>().Play();
        Destroy(newObj, newObj.GetComponent<AudioSource>().clip.length);
    }
}
