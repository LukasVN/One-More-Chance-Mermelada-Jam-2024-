using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    float deltaTime = 0.0f;
    private void Start() {
        QualitySettings.vSyncCount = 0;  // Disable vSync
        Application.targetFrameRate = 61; // Set target FPS

        if(PlayerPrefs.HasKey(musicAudioSource.name)){
            musicAudioSource.volume = PlayerPrefs.GetFloat(musicAudioSource.name);
        }
        if(PlayerPrefs.HasKey(sfxAudioSource.name)){
            sfxAudioSource.volume = PlayerPrefs.GetFloat(sfxAudioSource.name);
        }
    }
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    // void OnGUI()
    // {
    //     int w = Screen.width, h = Screen.height;

    //     GUIStyle style = new GUIStyle();

    //     Rect rect = new Rect(0, 0, w, h * 2 / 100);
    //     style.alignment = TextAnchor.UpperLeft;
    //     style.fontSize = h * 2 / 100;
    //     style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
    //     float msec = deltaTime * 1000.0f;
    //     float fps = 1.0f / deltaTime;
    //     string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    //     GUI.Label(rect, text, style);
    // }
    
}
