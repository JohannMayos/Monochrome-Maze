using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Transform[] pos; 
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Switch.isPressed == false || transform.position.x <= pos[1].position.x){
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        
        if(Switch.isPressed == true || transform.position.x <= pos[0].position.x){
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }
}
