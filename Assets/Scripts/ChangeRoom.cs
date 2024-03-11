using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public GameObject player;
    public Transform newPlayerPosition;
    public GameObject roomEnemies;
    private bool enemiesSpawned = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !enemiesSpawned){
            enemiesSpawned = true;
            Invoke("spawnEnemies",1f);
        }
    }

    private void spawnEnemies(){
        roomEnemies.SetActive(true);
        player.transform.position = newPlayerPosition.position;
    }
}