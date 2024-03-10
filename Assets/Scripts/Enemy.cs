using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    protected float speed = 10f;
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
        Vector2 direction = player.position - transform.position;
        if (direction.magnitude > 2)
        {
            Vector2 velocity = direction.normalized * speed * Time.deltaTime;
            rb.MovePosition((Vector2)transform.position + velocity);
        }
    }

    public virtual void ReceiveDamage(Vector2 pushDirection){
        StartCoroutine(DamageAnimation());
    }

    protected IEnumerator DamageAnimation(){
    for(int i = 0; i < 5; i++){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
    }
        spriteRenderer.color = initialColor;
    }
}

