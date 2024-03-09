using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    private float speed = 10f;
    private Rigidbody2D rb;
    private float pushBackForce = 1f;
    private bool isPushed;
    private float pushCooldown = 0.25f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        if(isPushed){
            pushCooldown -= Time.deltaTime;
            if (pushCooldown <= 0)
                {
                    isPushed = false;
                }
            return;
        }
        Vector2 direction = player.position - transform.position;
        if (direction.magnitude > 2)
        {
            Vector2 velocity = direction.normalized * speed * Time.deltaTime;
            rb.MovePosition((Vector2)transform.position + velocity);
        }
    }

    public void ReceiveDamage(Vector2 pushDirection){
        Debug.Log("Hit");
        rb.AddForce(pushDirection / 10 * pushBackForce, ForceMode2D.Impulse);
        isPushed = true;
        pushCooldown = 1f;
    }
}
