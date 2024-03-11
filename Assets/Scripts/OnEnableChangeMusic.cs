using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableChangeMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip newSong;
    private void OnEnable() {
        audioSource.Stop();
        audioSource.PlayOneShot(newSong);
    }
}
