using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Options : MonoBehaviour
{

    [SerializeField]
    private AudioSource testing;

    [SerializeField]
    private Slider volSlider;

    [SerializeField]
    private AudioMixer master;


    private float volDefault = 0.2f;


    public float currentVolume;

    private float attenuation;


    // Start is called before the first frame update
    void Start()
    {
        currentVolume = volDefault;
        volSlider.value = volDefault;
        master.SetFloat("masterVolume", (float)(Mathf.Log10(currentVolume) * 20));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeVolume()
    {
        currentVolume = volSlider.value;
        master.SetFloat("masterVolume", (float)(Mathf.Log10(currentVolume) * 20));

    }
}
