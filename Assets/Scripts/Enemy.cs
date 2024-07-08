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

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    private float stopTime;
    public float startStopTime;



    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        normalSpeed = speed;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (!isActive){
            return;
        }
        if (stopTime <= 0){
            speed = normalSpeed;
        }
        else{
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        
        if (health <= 0){
            Destroy(gameObject);
        }
        
        if (target.position.x < transform.position.x){
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else{ 
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    
    public void TakeDamage(int damage){
        stopTime = startStopTime;
        health -= damage;
    }
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
    public void OnEnemyAttack(int damage){
        animator.SetBool("enemyAttack", false);
        if (player.armourCount != 0){
            player.ChangeArmour(-damage);
        }
        else{
            player.ChangeHealth(-damage);
        }

        timeBtwAttack = startTimeBtwAttack;      
    }

    public void Activate()
    {
        isActive = true;
        // Додаткова логіка активації (наприклад, ввімкнення анімації)
    }

    public void Deactivate()
    {
        isActive = false;
        // Додаткова логіка деактивації (наприклад, вимкнення анімації)
    }
}
