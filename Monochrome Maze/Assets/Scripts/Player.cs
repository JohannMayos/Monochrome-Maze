using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Animator anim;
    private Rigidbody2D rig;
    public float JumpForce;
    public bool isJumping;
    public Transform Weapon;
    public int maxHealth = 500;
    public int currentHealth;
    public HealthBar healthbar;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public static Player player;
    public SpriteRenderer sprite;
    
    private void Awake(){
        
        if(player == null){
            player = this;
        }

        else{
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); 

    }
   
    void Start(){
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        
    }
    

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Attack();
    }


    void OnCollisionEnter2D(Collision2D col){

        if (col.gameObject.layer == 8)
        {
            isJumping = false;
            
        }

        if(col.gameObject.tag == "Traps"){
            TakeDamage(500);
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    
    }


    public IEnumerator DamagePlayer(){
        anim.SetBool("Damage", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Damage", false);

        for(int i = 0; i < 7; i++){
            sprite.enabled = false;
            yield return new WaitForSeconds(0.15f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }

        BodyTrigger.body.enabled = true;
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
        GameController.instance.ShowGameOver();
        
    }


    void Move(){
        Vector3 movement =  new Vector3(Input.GetAxis("Horizontal") ,0f,0f);
        transform.position = transform.position + movement * speed * Time.deltaTime;

        if(Input.GetAxis("Horizontal") > 0f){
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        
        }


        if(Input.GetAxis("Horizontal") < 0f){
            anim.SetBool("Walk", true);
             transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if(Input.GetAxis("Horizontal") == 0f){
            anim.SetBool("Walk", false);
        }

        
        
    }

    void Jump(){
    
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }


    void Attack(){

        if(Input.GetButtonDown("Fire1")){
            anim.SetTrigger("Attack");

            
        }
    }

    void Damage(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Weapon.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Boss>().TakeDamage(100);
                
            }
    }

    void OnDrawGizmosSelected(){
        if(Weapon == null){
            return;
        }

        Gizmos.DrawWireSphere(Weapon.position, attackRange);
    }
}
