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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        normalSpeed = speed;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        //якщо гравець не увійшов до кімнати, поле isActive обриває ітерації циклу, не даючи цим ворогу рухатись.
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
        
        //mirror enemy object according to target position
        if (target.position.x < transform.position.x){
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else{ 
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        //move towards target
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    

    //метод обробки отриманого урону.
    public void TakeDamage(int damage){
        health -= damage;
        
        //маленька пауза в момент отримання урону
        stopTime = startStopTime;
    }

    //атака гравця, якщо той поруч і перезарядка атаки вже закінчена.
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

    //метод, що передає гравцю урон.
    public void OnEnemyAttack(){
        animator.SetBool("enemyAttack", false);
        player.ChangeArmour(-damage);

        timeBtwAttack = startTimeBtwAttack;      
    }

    //методи доступу до поля isActive.
    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
