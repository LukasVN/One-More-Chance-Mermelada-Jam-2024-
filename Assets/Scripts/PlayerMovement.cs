using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Transform hitController;
    private Animator animator;
    public float speed = 15f;
    public float hitRadius;
    private float attackCooldown = 0.5f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(attackCooldown > 0){
            attackCooldown -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space) && attackCooldown <= 0){
            Hit();
            attackCooldown = 0.5f;
        }
    }

    void LateUpdate()
    {
        Debug.Log(rb.velocity.x); 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontalInput,0f);
        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);

        if (movementDirection.x != 0) {
            spriteRenderer.flipX = movementDirection.x > 0f;
        }
        

        #region HitColliderPositioning
            if(movementDirection.x != 0){
                if(spriteRenderer.flipX){
                    hitController.transform.localPosition = new Vector3(0.275f, 0, 0); // Move to left side
                    animator.SetBool("MovingSide",true);
                }
                else{
                    hitController.transform.localPosition = new Vector3(-0.275f, 0, 0); // Move to right side
                    animator.SetBool("MovingSide",true);
                }
            }
            if(movementDirection.y != 0){
                if(movementDirection.y > 0){
                    hitController.transform.localPosition = new Vector3(0, 0.3f, 0); // Move up
                    animator.SetBool("MovingBack",true);
                    animator.SetBool("MovingSide",false);
                    animator.SetBool("MovingFront",false);
                }
                else{
                    hitController.transform.localPosition = new Vector3(0, -0.3f, 0); // Move down
                    animator.SetBool("MovingFront",true);
                    animator.SetBool("MovingBack",false);
                    animator.SetBool("MovingSide",false);
                }    
            }
        #endregion HitColliderPositioning
        
        if(Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y)){
            animator.SetBool("MovingSide",true);
            animator.SetBool("MovingBack",false);
            animator.SetBool("MovingFront",false);
        }
        else if(rb.velocity.x == 0 && rb.velocity.y > 0){
            animator.SetBool("MovingSide",false);
            animator.SetBool("MovingFront",true);
            hitController.transform.localPosition = new Vector3(0, -0.3f, 0); // Move down
        }

        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement/3);
        
    }
    

    private void Hit(){
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitController.position, hitRadius);
        animator.ResetTrigger("AttackingSide");
        animator.ResetTrigger("AttackingFront");
        animator.ResetTrigger("AttackingBack");

        if(animator.GetBool("MovingSide")){
            animator.SetTrigger("AttackingSide");
        }
        else if(animator.GetBool("MovingFront")){
            animator.SetTrigger("AttackingFront");
        }
        else if(animator.GetBool("MovingBack")){
            animator.SetTrigger("AttackingBack");
        }

        foreach (Collider2D collider in hits)
        {
            if(collider.CompareTag("Enemy")){
                Vector2 pushDirection = (collider.transform.position - transform.position).normalized;
                collider.GetComponent<Enemy>().ReceiveDamage(pushDirection);
            }
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(hitController.position,hitRadius);
    }
    
}
