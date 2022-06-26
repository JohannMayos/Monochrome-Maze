using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   

    public float speed;
    public float distance;
    bool isRight = true;
    private Rigidbody2D rig;
    


    public Transform groundCheck;

    public LayerMask layer;

    public BoxCollider2D boxCollider2D;

    void Start(){
        rig = GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);


        if(ground.collider == false){
            if(isRight == true){
                transform.eulerAngles = new Vector3 (0f, 0f, 0f);
                isRight = false;
            }
            else{
                transform.eulerAngles = new Vector3 (0f, 180f, 0f);
                isRight = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Weapon"){
            speed = 0;
            boxCollider2D.enabled = false;
            rig.bodyType = RigidbodyType2D.Kinematic;
            Destroy(this.gameObject);
        }

    }

    
}
