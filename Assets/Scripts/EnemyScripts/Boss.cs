using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public GameObject[] enemies;
    public Transform enemyPositionLeft;
    public Transform enemyPositionRight;
    protected float pushBackForce = 1.5f;
    protected bool isPushed = false;
    protected float pushCooldown = 2f;
    protected float habilityCooldown = 5f;
    protected bool usingHability;
    public GameObject projectilePrefab;
    protected bool castingProjectiles = false;
    protected float castingCooldown = 2f;
    protected bool spawningEnemies = false;
    protected float spawningCooldown = 2f;
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        if(!castingProjectiles && !spawningEnemies){
            habilityCooldown -= Time.deltaTime;
        }
        if(habilityCooldown <= 0){
            usingHability = true;
        }
        if(castingProjectiles){
            castingCooldown -= Time.deltaTime;
            if(castingCooldown <= 0){
                castingProjectiles = false;
                animator.SetBool("Casting",false);
            }
        }
        if(spawningEnemies){
            spawningCooldown -= Time.deltaTime;
            if(spawningCooldown <= 0){
                spawningEnemies = false;
                animator.SetBool("Spawning",false);
            }
        }
        if(isPushed){
            pushCooldown -= Time.deltaTime;
            if (pushCooldown <= 0)
                {
                    isPushed = false;
                }
            return;
        }

        
        if(usingHability){
            UseHability(Random.Range(1,3));
            usingHability = false;
        }
        else{
            Vector2 direction = player.position - transform.position;
            if (direction.magnitude > 2 && !castingProjectiles && !spawningEnemies)
            {
                Vector2 velocity = direction.normalized * speed * Time.deltaTime;
                rb.MovePosition((Vector2)transform.position + velocity);
            }
        }
        
    }

    public override void ReceiveDamage(Vector2 pushDirection, int damage){
        base.ReceiveDamage(pushDirection, damage);
        rb.velocity = Vector2.zero;
        rb.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        isPushed = true;
        pushCooldown = 0.4f;
    }

    protected virtual void UseHability(int habilityNum){
        switch (habilityNum)
        {
            case 1: ThrowProjectiles();
            break;
            case 2: SpawnEnemies();
            break;
        }
    }

    protected virtual void ThrowProjectiles(){
        animator.SetBool("Casting",true);
        castingProjectiles = true;
        rb.velocity = Vector2.zero;
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        Vector3 positionOneUnitInFront = transform.position + direction * 2;
        Instantiate(projectilePrefab, positionOneUnitInFront + new Vector3(positionOneUnitInFront.x / 2,0,0),Quaternion.identity);
        Instantiate(projectilePrefab, positionOneUnitInFront,Quaternion.identity);
        Instantiate(projectilePrefab, positionOneUnitInFront + new Vector3(positionOneUnitInFront.x *2,0,0),Quaternion.identity);
        castingCooldown = 2f;
    }

    protected virtual void SpawnEnemies(){
        animator.SetBool("Spawning",true);
        spawningEnemies = true;
        Instantiate(enemies[Random.Range(0,enemies.Length)],enemyPositionLeft.position,Quaternion.identity);
        Instantiate(enemies[Random.Range(0,enemies.Length)],enemyPositionRight.position,Quaternion.identity);
        habilityCooldown = 15f;

    }

    
}
