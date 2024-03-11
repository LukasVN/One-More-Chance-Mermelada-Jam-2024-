using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeChanger : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider sliderToChange;
    public Text sliderValueText;
    private void Start() {
        if(PlayerPrefs.HasKey(audioSource.name)){
            sliderValueText.text = Mathf.Round(PlayerPrefs.GetFloat(audioSource.name) * 100)+"";
            sliderToChange.value = PlayerPrefs.GetFloat(audioSource.name);
            audioSource.volume = sliderToChange.value;
        }
    }
    public void ChangeVolume(){
        sliderValueText.text = Mathf.Round(sliderToChange.value * 100)+"";
        audioSource.volume = sliderToChange.value;
        PlayerPrefs.SetFloat(audioSource.name,audioSource.volume);
    }
}
