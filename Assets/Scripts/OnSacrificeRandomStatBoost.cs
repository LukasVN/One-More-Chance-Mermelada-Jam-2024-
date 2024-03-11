using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSacrificeRandomStatBoost : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && player.GetComponent<PlayerMovement>().health - 25 > 0){
            player.GetComponent<PlayerMovement>().health -=25;
            player.GetComponent<PlayerMovement>().speed += 10;
            player.GetComponent<PlayerMovement>().damage += 10;
            Destroy(gameObject);
        }
    }
}
