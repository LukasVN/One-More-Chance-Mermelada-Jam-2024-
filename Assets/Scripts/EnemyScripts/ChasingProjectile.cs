using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingProjectile : EnemyProjectile
{
    private Transform player;
    protected Vector2 direction;
    protected Rigidbody2D rb;
    public float speed = 5f;
    public float timeAlive = 1.5f;

    protected virtual void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        direction = player.position - transform.position;
        Destroy(gameObject,timeAlive);
    }

    protected virtual void LateUpdate()
    {
        Vector2 velocity = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition((Vector2)transform.position + velocity);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.CompareTag("Environment") || other.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
