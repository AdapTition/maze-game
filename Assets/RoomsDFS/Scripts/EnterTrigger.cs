using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEditor.Rendering;
using UnityEngine;
public class RoomTrigger : MonoBehaviour
{
    public Enemy[] enemies; // Массив ворогів у кімнаті
    public GameObject[] blocks;
    private int enemiesInTheRoom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Активуємо ворогів при вході гравця в кімнату
            foreach (var enemy in enemies)
            {
                enemy.Activate();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Деактивуємо ворогів при виході гравця з кімнати
            foreach (var enemy in enemies)
            {
                enemy.Deactivate();
            }
        }
    }
    int FindObjectsOfTypeWithTag(string tag)
    {
        int enemiesInTheRoom = 0;
        foreach (Transform transform in Resources.FindObjectsOfTypeAll(typeof(Transform)) as Transform[])
        {
            if (transform.hideFlags == HideFlags.None && transform.CompareTag(tag))
            {
                enemiesInTheRoom ++;
            }
        }
        return enemiesInTheRoom;
    }
}