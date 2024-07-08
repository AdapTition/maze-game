using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    
    void Update(){
    // Check if the bullet hit an object in the "Solid" layer and deal damage to the enemy if hit
    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
    if (hitInfo.collider != null){
        // If the hit object is tagged as "Enemy", apply damage
        if(hitInfo.collider.CompareTag("Enemy")){
            hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
        }
        // Destroy the bullet after hitting any object
        Destroy(gameObject);
    }

    // Move the bullet
    transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
