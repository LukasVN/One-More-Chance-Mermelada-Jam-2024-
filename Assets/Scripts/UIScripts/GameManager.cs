using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake() {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  
            Application.targetFrameRate = 60;
        #endif
    }
    void Start()
    {

    }

    void Update()
    {
        
        
    }
    
}
