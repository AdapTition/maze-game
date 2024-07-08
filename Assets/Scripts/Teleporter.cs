using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;

    //Method that teleports player to the destination objects's position.
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            other.transform.position = destination.GetComponent<Transform>().position;
        }
    }
}
