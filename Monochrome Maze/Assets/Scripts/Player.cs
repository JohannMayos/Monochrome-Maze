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
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    
   
    void Start(){

        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
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

        if(col.gameObject.tag == "Enemy"){
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "Traps"){
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

     void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
        
            GameController.instance.ShowGameOver();
            Destroy(col.gameObject);
            Destroy(this.gameObject);
            
        }
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

   void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {

                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
               
           
        }


    }


    void Attack(){

        if(Input.GetButtonDown("Fire1")){
            anim.SetTrigger("Attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Weapon.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("hit");
            }
        }
    }
}
