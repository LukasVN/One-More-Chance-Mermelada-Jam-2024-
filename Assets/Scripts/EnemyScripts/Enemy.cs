using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 8f;
    public int health = 60;
    public int damage = 10;
    public GameObject deathAnim;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Color initialColor;

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
    }

    protected virtual void LateUpdate()
    {
        if(health == 0 && spriteRenderer.enabled){
            speed = 0;
            spriteRenderer.enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            GameObject death = Instantiate(deathAnim, transform.position,quaternion.identity);
            Destroy(death,0.4f);
            Destroy(gameObject,0.5f);
        }
    }

    public virtual void ReceiveDamage(Vector2 pushDirection, int damage){
        StartCoroutine(DamageAnimation());
        if(health - damage <= 0){
            health = 0;
        }
        else{
            health -= damage;
        }
    }

    protected IEnumerator DamageAnimation(){
    for(int i = 0; i < 5; i++){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
    }
        spriteRenderer.color = initialColor;
    }

    protected void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerMovement>().ReceiveDamage(damage);
        }
    }

    protected void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerMovement>().ReceiveDamage(damage);
        }
    }

}

