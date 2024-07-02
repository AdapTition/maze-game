using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1;
    private Animator anim;


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

        if(moveInput.x != 0 || moveInput.y != 0  ){
            anim.SetBool("isRunning", true);
        }
        else{
            anim.SetBool("isRunning", false);
        }

    }
}