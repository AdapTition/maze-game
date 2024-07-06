using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    private float normalSpeed;
    public int damage;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    private float stopTime;
    private float startStopTime;
    private Player player;
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        normalSpeed = speed;
    }
    void Update()
    {
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
        transform.Translate(Vector2.left* speed * Time.deltaTime);
    }
    
    public void TakeDamage(int damage){
        health -= damage;
    }
    private void OnTriggerStay2D(Collider2D other){
        if (other.CompareTag("Player")){
            if(timeBtwAttack <= 0){
                animator.SetTrigger("attack");
            }
            else{
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    public void OnEnemtAttack(int damage){
        if (player.armourCount != 0){
            int a = damage;
            damage -= player.armourCount;
            player.armourCount -= a;
        }
        player.healthCount -= damage;
    }
}
