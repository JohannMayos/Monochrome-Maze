using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public Transform[] pos; 
    public float speed = 2f;
    public static BossDoor door;

    // Update is called once per frame
    void Update()
    {
       Open();
       Close();
    }


    public void Open(){
        if(DoorTrigger.isPressed == false || transform.position.y <= pos[1].position.y){
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }

    public void Close(){
        if(DoorTrigger.isPressed == true || transform.position.y <= pos[0].position.y){
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }
}
