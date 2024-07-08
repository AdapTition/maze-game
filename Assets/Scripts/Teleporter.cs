using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;

    //метод, що телепортує гравця в позицію об'єкту destination.
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            other.transform.position = destination.GetComponent<Transform>().position;
        }
    }
}
