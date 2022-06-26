using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    private Animator anim;
    public int maxHealth = 1000;
    public int currentHealth;
    public HealthBar healthbar;
    private bool flipX = false;
    public Vector3 attackOffset;
    public LayerMask attackMask;
    public float attackRange = 1f;
    public Transform Sword;
    

    void Start(){

        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }


    public void LookAtPlayer(){

        if(transform.position.x > player.position.x && !isFlipped){
           
            FlipBullet();
            isFlipped = true;
        }
        else{
            if(transform.position.x < player.position.x && isFlipped){
                
                FlipBullet();
                isFlipped = false;
                
            }
        }
    }

    public void Attack(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Sword.position, attackRange, attackMask);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Player>().TakeDamage(50);
                
            }
    }

    void OnDrawGizmosSelected(){
        if(Sword == null){
            return;
        }

        Gizmos.DrawWireSphere(Sword.position, attackRange);
    }

    public void FlipBullet(){

        flipX = !flipX;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        

    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);

        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        anim.SetTrigger("Die");
        Destroy(gameObject, 1f);
        GameController.instance.ShowVictory();
    }

   
}
