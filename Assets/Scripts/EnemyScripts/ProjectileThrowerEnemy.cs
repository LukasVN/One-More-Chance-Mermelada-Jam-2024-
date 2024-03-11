using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileThrowerEnemy : Enemy
{
    protected Vector2 direction;
    public GameObject projectilePrefab;
    private GameObject projectile;
    public Sprite onRangeSprite;
    public float detectingDistance = 15f;
    private float timer = 0;
    private Animator animator;
    public float shootDelay = 1f; 

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    
    protected override void LateUpdate()
    {
        base.LateUpdate();
        
        timer -= Time.deltaTime;
        
        if(Vector2.Distance(transform.position,player.position) <= detectingDistance ){
            if(projectile == null && spriteRenderer.enabled && timer <= 0){
                animator.SetBool("isMoving",false);
                spriteRenderer.sprite = onRangeSprite;
                ThrowProjectile();
                timer = shootDelay; 
            }
        }
        else if(Vector2.Distance(transform.position,player.position) > detectingDistance){
            if(!animator.GetBool("isMoving")){
                animator.SetBool("isMoving",true);
            }
            Vector2 direction = player.position - transform.position;
            Vector2 velocity = direction.normalized * speed * Time.deltaTime;
            rb.MovePosition((Vector2)transform.position + velocity);
        }

        
        
    }

    protected virtual void ThrowProjectile(){
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        Vector3 positionOneUnitInFront = transform.position + direction * 2;
        projectile = Instantiate(projectilePrefab, positionOneUnitInFront,quaternion.identity);
    }

}
