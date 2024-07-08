using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEditor.Rendering;
using UnityEngine;
public class RoomTrigger : MonoBehaviour
{
    public Enemy[] enemies; // Массив ворогів у кімнаті
    private int enemiesInTheRoom;


    // Змінюємо значення поля isActive класу Enemy, якщо гравець зайшов на тригер.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            foreach (var enemy in enemies)
            {
                enemy.Activate();
            }
        }

    }

    // Деактивуємо ворогів при виході гравця з кімнати
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            foreach (var enemy in enemies)
            {
                enemy.Deactivate();
            }
        }
    }
}