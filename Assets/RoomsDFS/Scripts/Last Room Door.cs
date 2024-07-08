using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastRoomDoor : MonoBehaviour
{
    public Player player;
    public GameObject block;
    
    // Destroy the last room door blocks, if player had reached the key.
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Player>().IsKeyReached()){
                Destroy(block);
            }
        }
    }
}
