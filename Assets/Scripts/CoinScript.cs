using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    
    private TextMeshProUGUI coinCounter;
    private int coinCounterValue;
    public int coinValue;

    private void Start() {
        coinCounter = GameObject.Find("CoinCounterText").GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
        bool success = int.TryParse(coinCounter.text.Trim(), out coinCounterValue);
        if (success)
        {
            if(coinCounterValue + coinValue > 99){
                coinCounterValue = 99;
            }
            else{
                coinCounterValue = coinCounterValue + coinValue;
                coinCounter.text = coinCounterValue+"";
                GameObject.Find("Player").GetComponent<PlayerMovement>().SetCoins(coinCounterValue);
                Destroy(gameObject);
            }
        }
        }
    }
}
