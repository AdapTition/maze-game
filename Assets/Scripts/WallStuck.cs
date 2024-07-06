// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WallStuck : MonoBehaviour
// {
//     // Радіус пошуку блоків стін навколо дверей
//     public float checkRadius = 0.5f;

//     public void Update()
//     {
//         // Знаходимо всі двері на сцені
//         GameObject[] walls = FindObjectsOfTypeWithTag("Wall");

//         foreach (GameObject wall in walls)
//         {
//             RemoveNearbyWalls(wall);
//         }
//     }

//     GameObject[] FindObjectsOfTypeWithTag(string tag)
//     {
//         List<GameObject> taggedObjects = new List<GameObject>();
//         foreach (Transform transform in Resources.FindObjectsOfTypeAll(typeof(Transform)) as Transform[])
//         {
//             if (transform.hideFlags == HideFlags.None && transform.CompareTag(tag))
//             {
//                 taggedObjects.Add(transform.gameObject);
//             }
//         }
//         return taggedObjects.ToArray();
//     }

//     void RemoveNearbyWalls(GameObject wall)
//     {
//         // Знаходимо всі блоки стін навколо дверей
//         Collider2D[] badwalls = Physics2D.OverlapCircleAll(wall.transform.position, checkRadius);

//         foreach (Collider2D badwall in badwalls)
//         {
//             if (wall.CompareTag("Wall"))
//             {
//                 Destroy(badwall.gameObject);
//                 Instantiate(badwall.gameObject);
//             }
//         }
//     }
// }