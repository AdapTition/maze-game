using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastRoomDoor : MonoBehaviour
{
    public Player player;
    public GameObject block;
    
    // Якщо гравець отримаав ключ -- знищуємо блоки, що закривають прохід до порталу.
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Player>().IsKeyReached()){
                Destroy(block);
            }
        }
    }
}
