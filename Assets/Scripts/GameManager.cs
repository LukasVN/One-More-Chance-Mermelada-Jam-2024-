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

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
}
