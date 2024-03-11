using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToChange;
    void OnEnable()
    {
        Invoke("ChangeScene",2);
    }

    // Update is called once per frame
    
    private void ChangeScene(){
        SceneManager.LoadScene(sceneToChange);
    }
}
