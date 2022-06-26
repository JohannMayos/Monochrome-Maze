using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public static bool isPressed;

    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            isPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            isPressed = false;
        }
    }
}
