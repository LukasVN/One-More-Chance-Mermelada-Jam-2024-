using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject gameOver;
    public string currentScene;
    public string menuScene;
    public AudioClip spellCast;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Transform hitController;
    public HealthBar healthBar;
    public GameObject MagicOrb;
    private Animator animator;
    public int health = 100;
    public int damage = 20;
    public float speed = 15f;
    public float hitRadius;
    private float attackCooldown = 0.5f;
    public bool inmunity = false;
    public int coins = 0;
    private bool isGameOver;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(isGameOver){
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit")){
                isGameOver = false;
                gameOver.SetActive(false);
                Time.timeScale = 1;
                SceneManager.LoadScene(currentScene);
            }
            else if(Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.JoystickButton7)){
                SceneManager.LoadScene(menuScene);
            }
            return;
        }
        if(attackCooldown > 0){
            attackCooldown -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space) && attackCooldown <= 0 || Input.GetButtonDown("Submit") && attackCooldown <= 0){
            Hit();
            audioSource.Stop();
            audioSource.PlayOneShot(spellCast);
            attackCooldown = 0.5f;
        }
    }

    void LateUpdate()
    {
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
        if(rb.velocity.x == 0 && rb.velocity.y == 0 && !animator.GetBool("MovingBack") && !animator.GetBool("AttackingBack")){
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

        GameObject attackOrb = Instantiate(MagicOrb, hitController.position, Quaternion.identity);
        Destroy(attackOrb, 0.5f);

        foreach (Collider2D collider in hits)
        {
            if(collider.CompareTag("Enemy") && !collider.isTrigger){
                Vector2 pushDirection = (collider.transform.position - transform.position).normalized;
                collider.GetComponent<Enemy>().ReceiveDamage(pushDirection,damage);
            }
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(hitController.position,hitRadius);
    }

    public void ReceiveDamage(int damage)
    {
        Debug.Log("Damage");
        if(!inmunity){
            if(health - damage <= 0){
                StartCoroutine(DamageAnimation());
                health = 0;
                healthBar.SetValue(health);
                SetGameOver();
        }
            else{
                StartCoroutine(DamageAnimation());
                health -= damage;
                healthBar.SetValue(health);
                inmunity = true;
                Invoke("ResetInmunity",1);
                StartCoroutine(InmunityAnimation());
                
            }
        }
        
    }

    protected IEnumerator DamageAnimation(){
    for(int i = 0; i < 5; i++){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
    }
        spriteRenderer.color = Color.white;
    }

    protected IEnumerator InmunityAnimation(){
        Color originalColor = Color.white;
        float duration = 1f; // Duration for fade in/out

        // Fade out
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, t));
            spriteRenderer.color = newColor;
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        yield return new WaitForSeconds(0.1f);

        // Fade in
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, t));
            spriteRenderer.color = newColor;
            yield return null;
        }

        spriteRenderer.color = originalColor;
    }

    private void ResetInmunity(){
        inmunity = false;
    }

    public void SetCoins(int newValue){
        coins = newValue;
    }

    private void SetGameOver(){
        Time.timeScale = 0;
        animator.ResetTrigger("AttackingSide");
        animator.ResetTrigger("AttackingFront");
        animator.ResetTrigger("AttackingBack");
        animator.SetBool("MovingSide",false);
        animator.SetBool("MovingBack",false);
        animator.SetBool("MovingFront",false);
        animator.SetBool("GameOver",true);
        Camera.main.orthographicSize = 2;
        isGameOver = true;
        gameOver.SetActive(true);
    }
    
}
