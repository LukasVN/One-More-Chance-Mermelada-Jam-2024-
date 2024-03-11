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
    }

    protected virtual void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            if(!other.gameObject.GetComponent<PlayerMovement>().inmunity){
                other.gameObject.GetComponent<PlayerMovement>().ReceiveDamage(damage);
            }
        }
    }

}
