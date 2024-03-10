using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : Enemy
{
    protected float pushBackForce = 5f;
    protected bool isPushed = false;
    protected float pushCooldown = 0.4f;

    protected override void LateUpdate()
    {
        if(isPushed){
            pushCooldown -= Time.deltaTime;
            if (pushCooldown <= 0)
                {
                    isPushed = false;
                }
            return;
        }

        base.LateUpdate();
        
        Vector2 direction = player.position - transform.position;
        if (direction.magnitude > 2)
        {
            Vector2 velocity = direction.normalized * speed * Time.deltaTime;
            rb.MovePosition((Vector2)transform.position + velocity);
        }
    }

    public override void ReceiveDamage(Vector2 pushDirection, int damage){
        base.ReceiveDamage(pushDirection, damage);
        rb.velocity = Vector2.zero;
        rb.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        isPushed = true;
        pushCooldown = 0.4f;
    }


}

