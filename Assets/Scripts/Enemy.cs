using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    protected float speed = 10f;
    protected Rigidbody2D rb;

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
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
        Debug.Log("Hit Generic");
    }

}

