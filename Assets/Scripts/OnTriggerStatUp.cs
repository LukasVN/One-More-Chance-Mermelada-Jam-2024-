using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnTriggerStatUp : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip purchaseSound;
    private GameObject player;
    private TextMeshProUGUI coinCounter;
    public string modifiedStat;
    public int boostValue;
    public int price;
    private int coinCounterValue;
    void Start()
    {
        player = GameObject.Find("Player");
        coinCounter = GameObject.Find("CoinCounterText").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && player.GetComponent<PlayerMovement>().coins >= price){
            switch (modifiedStat)
            {
                case "speed": player.GetComponent<PlayerMovement>().speed = player.GetComponent<PlayerMovement>().speed  + boostValue;
                    PurchaseItem();
                    audioSource.Stop();
                    audioSource.PlayOneShot(purchaseSound);
                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponent<CircleCollider2D>().enabled = false;
                    Destroy(gameObject,1f);
                break;
                case "damage": player.GetComponent<PlayerMovement>().damage = player.GetComponent<PlayerMovement>().damage  + boostValue;
                    PurchaseItem();
                    audioSource.Stop();
                    audioSource.PlayOneShot(purchaseSound);
                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponent<CircleCollider2D>().enabled = false;
                    Destroy(gameObject,1f);
                break;
                case "health": 
                    if(player.GetComponent<PlayerMovement>().health + boostValue > 100){
                        player.GetComponent<PlayerMovement>().health = 100;
                        player.GetComponent<PlayerMovement>().healthBar.SetValue(100);
                        PurchaseItem();
                        GameObject.Find("Player").GetComponent<PlayerMovement>().SetCoins(player.GetComponent<PlayerMovement>().coins);
                        audioSource.Stop();
                        audioSource.PlayOneShot(purchaseSound);
                        GetComponent<SpriteRenderer>().enabled = false;
                        GetComponent<CircleCollider2D>().enabled = false;
                        Destroy(gameObject,1f);
                    }
                    else{
                        player.GetComponent<PlayerMovement>().healthBar.SetValue(player.GetComponent<PlayerMovement>().health  + boostValue);
                        player.GetComponent<PlayerMovement>().health = player.GetComponent<PlayerMovement>().health  + boostValue;
                        PurchaseItem();
                        audioSource.Stop();
                        audioSource.PlayOneShot(purchaseSound);
                        GameObject.Find("Player").GetComponent<PlayerMovement>().SetCoins(player.GetComponent<PlayerMovement>().coins);
                        GetComponent<SpriteRenderer>().enabled = false;
                        GetComponent<CircleCollider2D>().enabled = false;
                        Destroy(gameObject,1f);
                    }
                break;
            }
            
        }
    }

    private void PurchaseItem(){
        bool success = int.TryParse(coinCounter.text.Trim(), out coinCounterValue);
        if (success)
        {
            coinCounterValue = coinCounterValue - price;
            coinCounter.text = coinCounterValue+"";
            GameObject.Find("Player").GetComponent<PlayerMovement>().SetCoins(coinCounterValue);
        }
    }
}
