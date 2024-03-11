using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemyCounter : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject door;
    private bool roomCleared = false;
    void LateUpdate()
    {
        if(!roomCleared && isArrayNull()){
            door.SetActive(false);
            roomCleared = true;
        }
    }

    private bool isArrayNull(){
        foreach (GameObject item in enemies)
        {
            if(item != null){
                return false;
            }
        }
        return true;
    }
}
