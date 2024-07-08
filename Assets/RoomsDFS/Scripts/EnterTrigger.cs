using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEditor.Rendering;
using UnityEngine;
public class RoomTrigger : MonoBehaviour
{
    // Array of enemies in the room
    public Enemy[] enemies;
    private int enemiesInTheRoom;

    // Change the isActive field of the Enemy class when the player enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate all enemies in the room
            foreach (var enemy in enemies)
            {
                enemy.Activate();
            }
        }
    }

    // Deactivate enemies when the player leaves the room
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Deactivate all enemies in the room
            foreach (var enemy in enemies)
            {
                enemy.Deactivate();
            }
        }
    }
}
