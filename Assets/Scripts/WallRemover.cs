using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRemover : MonoBehaviour
{
    // Радіус пошуку блоків стін навколо дверей
    public float checkRadius = 0.5f;

    public void Update()
    {
        // Знаходимо всі двері на сцені
        GameObject[] doors = FindObjectsOfTypeWithTag("Door");

        foreach (GameObject door in doors)
        {
            RemoveNearbyWalls(door);
        }
    }

    GameObject[] FindObjectsOfTypeWithTag(string tag)
    {
        List<GameObject> taggedObjects = new List<GameObject>();
        foreach (Transform transform in Resources.FindObjectsOfTypeAll(typeof(Transform)) as Transform[])
        {
            if (transform.hideFlags == HideFlags.None && transform.CompareTag(tag))
            {
                taggedObjects.Add(transform.gameObject);
            }
        }
        return taggedObjects.ToArray();
    }

    void RemoveNearbyWalls(GameObject door)
    {
        // Знаходимо всі блоки стін навколо дверей
        Collider2D[] walls = Physics2D.OverlapCircleAll(door.transform.position, checkRadius);

        foreach (Collider2D wall in walls)
        {
            if (wall.CompareTag("Wall"))
            {
                Destroy(wall.gameObject);
            }
        }
    }
}
