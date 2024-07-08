using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    private Transform target;
    private Animator animator;
    private Rigidbody2D rb;
    public int health;
    public float speed;
    private float normalSpeed;
    public int damage;
    private bool isActive;

    //затримка між ударами
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    //пауза при отриманні урону
    private float stopTime;
    public float startStopTime;


    void Start(){
        // Get the Rigidbody2D component for physics handling
        rb = GetComponent<Rigidbody2D>();
        
        // Get the Animator component for animations
        animator = GetComponent<Animator>();
        
        // Find the player object in the scene
        player = FindObjectOfType<Player>();
        
        // Store the normal speed value
        normalSpeed = speed;
        
        // Find the player's transform by tag
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        // If the player has not entered the room, exit the update method to prevent the enemy from moving
        if (!isActive){
            return;
        }

        // If the stop time is over, set speed to normal speed, otherwise stop the enemy
        if (stopTime <= 0){
            speed = normalSpeed;
        }
        else{
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        // Destroy the enemy if health is 0 or below
        if (health <= 0){
            Destroy(gameObject);
        }

        // Mirror the enemy object according to the target position
        if (target.position.x < transform.position.x){
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else{ 
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Move towards the target
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    // Method to handle received damage
    public void TakeDamage(int damage){
        health -= damage;

        // Short pause when receiving damage
        stopTime = startStopTime;
    }

    // Attack the player if nearby and attack cooldown is over
    private void OnTriggerStay2D(Collider2D other){
        if (other.CompareTag("Player")){
            if(timeBtwAttack <= 0){
                animator.SetBool("enemyAttack", true);
            }
            else{
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    // Method to deal damage to the player
    public void OnEnemyAttack(){
        animator.SetBool("enemyAttack", false);
        player.ChangeArmour(-damage);

        // Reset attack cooldown
        timeBtwAttack = startTimeBtwAttack;      
    }

    // Methods to access the isActive field
    public void Activate(){
        isActive = true;
    }

    public void Deactivate(){
        isActive = false;
    }
}
