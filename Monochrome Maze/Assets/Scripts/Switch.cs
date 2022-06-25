using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    private Animator anim;
    public static bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){
            anim.SetBool("Hit", true);
            isPressed = true;
        }


    }


    private void OnTriggerExit2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){
            anim.SetBool("Hit", false);
            isPressed = false;
        }


    }
}
