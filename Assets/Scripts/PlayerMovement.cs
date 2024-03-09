using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float speed = 15f;
    public Transform hitController;
    public float hitRadius;
    private float attackCooldown = 0.5f;
    private Vector2 initialHitHorizontalPosition;
    private Vector2 initialHitVerticalPosition;
    public Sprite facingSide;
    public Sprite facingFront;
    public Sprite facingBack;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if(attackCooldown > 0){
            attackCooldown -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space) && attackCooldown <= 0){
            Hit();
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontalInput,0f);
        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);

        if (movementDirection.x != 0) {
            spriteRenderer.flipX = movementDirection.x < 0f;
        }

        #region HitColliderPositioning
            if(movementDirection.x != 0){
                if(spriteRenderer.flipX){
                    hitController.transform.localPosition = new Vector3(-0.275f, 0, 0); // Move to left side
                    spriteRenderer.sprite = facingSide;
                }
                else{
                    hitController.transform.localPosition = new Vector3(0.275f, 0, 0); // Move to right side
                    spriteRenderer.sprite = facingSide;
                }
            }
            if(movementDirection.y != 0){
                if(movementDirection.y > 0){
                    hitController.transform.localPosition = new Vector3(0, 0.3f, 0); // Move up
                    spriteRenderer.sprite = facingBack;
                }
                else{
                    hitController.transform.localPosition = new Vector3(0, -0.3f, 0); // Move down
                    spriteRenderer.sprite = facingFront;
                }    
            }
        #endregion HitColliderPositioning
        
        
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement/3);
        
    }
    

    private void Hit(){
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitController.position, hitRadius);

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
