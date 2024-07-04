using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D rb;
    private float speed = 0.3f;
    private Animator anim;
    public int health;


    void Start()
    {
        //запитуємо компонент Rigidbody для майбутньої взаємодії з ним через змінну rb
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        //Зберігаємо значення вектора напряму руху.
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        //змінюємо позицію rb, додаючи вектор напряму руху до її координат.
        rb.MovePosition(rb.position + moveInput * speed);

        // Перевіряємо чи рухається персонаж. Це можна використати для запуску анімації ходьби.
        if(moveInput.x != 0 || moveInput.y != 0  ){
            anim.SetBool("isRunning", true);
        }
        else{
            anim.SetBool("isRunning", false);
        }
    }
    /*public void OnTriggerEnter(){
        if(other.CompareTag("Potion")){
            ChangeHealth(1);
            Destroy(other.gameObject);
        }
    }

    public void ChangeHealth (int changeValue){
        health += changeValue;
    }
    */

}