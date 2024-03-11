using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerStatUp : MonoBehaviour
{
    private GameObject player;
    public string modifiedStat;
    public int boostValue;
    public int price;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && player.GetComponent<PlayerMovement>().coins >= price){
            Debug.Log("Trigger");
            switch (modifiedStat)
            {
                case "speed": player.GetComponent<PlayerMovement>().speed = player.GetComponent<PlayerMovement>().speed  + boostValue;
                    player.GetComponent<PlayerMovement>().coins = player.GetComponent<PlayerMovement>().coins - price;
                    Destroy(gameObject);
                break;
                case "damage": player.GetComponent<PlayerMovement>().damage = player.GetComponent<PlayerMovement>().damage  + boostValue;
                    player.GetComponent<PlayerMovement>().coins = player.GetComponent<PlayerMovement>().coins - price;
                    Destroy(gameObject);
                break;
                case "health": 
                    if(player.GetComponent<PlayerMovement>().health + boostValue > 100){
                        player.GetComponent<PlayerMovement>().health = 100;
                        player.GetComponent<PlayerMovement>().healthBar.SetValue(100);
                        player.GetComponent<PlayerMovement>().coins = player.GetComponent<PlayerMovement>().coins - price;
                        Destroy(gameObject);
                    }
                    else{
                        player.GetComponent<PlayerMovement>().healthBar.SetValue(player.GetComponent<PlayerMovement>().health  + boostValue);
                        player.GetComponent<PlayerMovement>().health = player.GetComponent<PlayerMovement>().health  + boostValue;
                        player.GetComponent<PlayerMovement>().coins = player.GetComponent<PlayerMovement>().coins - price;
                        Destroy(gameObject);
                    }
                break;
            }
            
        }
    }
}
