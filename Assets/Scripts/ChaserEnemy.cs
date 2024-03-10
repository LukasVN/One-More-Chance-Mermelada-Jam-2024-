using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : Enemy
{
    protected float pushBackForce = 5f;
    protected bool isPushed = false;
    protected float pushCooldown = 0.4f;
    protected override void Start()
    {
        base.Start();
    }

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
    }

    public override void ReceiveDamage(Vector2 pushDirection){
        base.ReceiveDamage(pushDirection);
        Debug.Log("Hit ChaserEnemy");
        rb.velocity = Vector2.zero;
        rb.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        isPushed = true;
        pushCooldown = 0.4f;
    }

}
