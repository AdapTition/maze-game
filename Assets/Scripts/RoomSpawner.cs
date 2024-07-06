using System.Collections;
using System.Collections.Generic;
using com.gstudios.mapgen;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    public enum Direction{
        Top,
        Bottom,
        Left,
        Right,
        None
    }

    private RoomVariants variants;
    private int rand;
    private bool spawned = false;
    private float waitTime = 3f;

    private void Start(){
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.3f);
    }
    public void Spawn(){
        if (!spawned){
            if(direction == Direction.Top){
                rand = Random.Range(0, variants.topRooms.Length);
                Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Bottom){
                rand = Random.Range(0, variants.bottomRooms.Length);
                Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Left){
                rand = Random.Range(0, variants.leftRooms.Length);
                Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Right){
                rand = Random.Range(0, variants.rightRooms.Length);
                Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation);
            }
            spawned = true;
            RemoveDuplicates();
            
        }   
    }
    private void OnTriggerStay2D(Collider2D other){
        if (other.GetComponent<RoomSpawner>().spawned && other.CompareTag("Room Point")){ 
            Destroy(gameObject);
        }
    }

    private void RemoveDuplicates()
    {
        // Знаходимо всі об'єкти з тегом "Wall"
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        
        // Використовуємо словник для зберігання унікальних координат
        Dictionary<Vector3, GameObject> uniqueBlock = new Dictionary<Vector3, GameObject>();

        foreach (GameObject wall in walls)
        {
            Vector3 position = wall.transform.position;

            // Якщо координата вже є в словнику, видаляємо об'єкт
            if (uniqueBlock.ContainsKey(position))
            {
                Destroy(wall);
            }
            else
            {
                // Якщо координата ще не зустрічалася, додаємо її в словник
                uniqueBlock.Add(position, wall);
            }
        }
        foreach (GameObject door in doors)
        {
            Vector3 position = door.transform.position;

            // Якщо координата вже є в словнику, видаляємо об'єкт
            if (uniqueBlock.ContainsKey(position))
            {
                Destroy(door);
            }
            else
            {
                // Якщо координата ще не зустрічалася, додаємо її в словник
                uniqueBlock.Add(position, door);
            }
        }

    }
    

    
}
