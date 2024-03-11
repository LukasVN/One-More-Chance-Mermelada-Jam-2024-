using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToChange;
    void OnEnable()
    {
        Invoke("ChangeScene",10);
    }

    private void LateUpdate() {
        if(Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKey(KeyCode.M)){
            ChangeScene();
        }
    }
    
    private void ChangeScene(){
        SceneManager.LoadScene(sceneToChange);
    }
}
