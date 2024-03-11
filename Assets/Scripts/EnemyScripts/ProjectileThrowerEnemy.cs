using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileThrowerEnemy : Enemy
{
    protected Vector2 direction;
    public GameObject projectilePrefab;
    private GameObject projectile;
    public float detectingDistance = 15f;
    private float timer = 0;
    public float shootDelay = 1f; 

    protected override void Start()
    {
        base.Start();
    }

    
    protected override void LateUpdate()
    {
        base.LateUpdate();
        
        timer -= Time.deltaTime;

        if(Vector2.Distance(transform.position,player.position) <= detectingDistance ){
            Debug.Log("In range");
            if(projectile == null && spriteRenderer.enabled && timer <= 0){
                ThrowProjectile();
                timer = shootDelay; 
        }
        else if(Vector2.Distance(transform.position,player.position) > detectingDistance){
            Debug.Log("Moving");
            Vector2 direction = player.position - transform.position;
            Vector2 velocity = direction.normalized * speed * Time.deltaTime;
            rb.MovePosition((Vector2)transform.position + velocity);
        }

        }
        
    }

    protected virtual void ThrowProjectile(){
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        Vector3 positionOneUnitInFront = transform.position + direction * 2;
        projectile = Instantiate(projectilePrefab, positionOneUnitInFront,quaternion.identity);
    }

}
