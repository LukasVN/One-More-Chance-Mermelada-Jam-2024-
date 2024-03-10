using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileThrowerEnemy : Enemy
{
    protected Vector2 direction;
    public GameObject projectilePrefab;
    private GameObject projectile;
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
        
        if(Vector2.Distance(transform.position,player.position) < 15 && projectile == null && spriteRenderer.enabled && timer <= 0){
            ThrowProjectile();
            timer = shootDelay; 
        }
    }

    protected virtual void ThrowProjectile(){
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        Vector3 positionOneUnitInFront = transform.position + direction * 2;
        projectile = Instantiate(projectilePrefab, positionOneUnitInFront,quaternion.identity);
    }

}
