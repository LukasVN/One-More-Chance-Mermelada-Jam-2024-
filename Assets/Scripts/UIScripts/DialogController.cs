using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] sentences;
    private int index = 0;
    public float dialogSpeed;
    public AudioClip dialogSound;
    public float pitch = 1f;
    public AudioSource audioSource;
    private Coroutine sentenceCoroutine;

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N) && sentenceCoroutine == null){
            NextSentence();
        }
    }

    void NextSentence(){
        dialogueText.text = "";
        if(index < sentences.Length){
            sentenceCoroutine = StartCoroutine(WriteSentence());
        }
    }

    IEnumerator WriteSentence(){
        int charIndex = 0;
        foreach (char character in sentences[index].ToCharArray())
        {
            dialogueText.text += character;
            if(charIndex % 2 == 0){
                if(audioSource.isPlaying){
                    audioSource.Stop();
                }
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(dialogSound);
            }
            yield return new WaitForSecondsRealtime(dialogSpeed);
            charIndex++;
        }            
        index++;   
        sentenceCoroutine = null;    

    }
}
