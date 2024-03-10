using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 15;

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            if(!other.gameObject.GetComponent<PlayerMovement>().inmunity){
                other.gameObject.GetComponent<PlayerMovement>().ReceiveDamage(damage);
            }
        }
        else if(other.gameObject.CompareTag("Environment")){
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            if(!other.gameObject.GetComponent<PlayerMovement>().inmunity){
                other.gameObject.GetComponent<PlayerMovement>().ReceiveDamage(damage);
            }
        }
    }

}
