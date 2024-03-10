using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasterEnemy : Enemy
{
    public GameObject damagingRay;
    public GameObject warningObject;
    public Sprite idleSprite;
    public Sprite warningSprite;
    public Sprite castingSprite;
    private bool isRayActive = false;
    
    //If the player stays totally still in the ray it doesn't damage
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && !isRayActive){
            spriteRenderer.sprite = warningSprite;
            warningObject.SetActive(true);
            Invoke("CastDamagingRay",0.75f);
            Invoke("DeactivateRay",2f);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && !isRayActive){
            warningObject.SetActive(true);
            spriteRenderer.sprite = warningSprite;
            Invoke("CastDamagingRay",0.75f);
            Invoke("DeactivateRay",2f);
        }
    }

    private void CastDamagingRay(){
        if(!isRayActive){
            spriteRenderer.sprite = castingSprite;
            damagingRay.SetActive(true);
            isRayActive = true;
        }
    }

    private void DeactivateRay(){
        if(isRayActive){
            spriteRenderer.sprite = idleSprite;
            damagingRay.SetActive(false);
            warningObject.SetActive(false);
            isRayActive = false;
        }
        
    }

}
