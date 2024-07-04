using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    public Player speed;
    
    Rigidbody2D cam_rb;
    private float cam_speed = 0.3f;
    internal static object main;

    void Start()
    {
        //запитуємо компонент Rigidbody для майбутньої взаємодії з ним через змінну rb
        cam_rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Зберігаємо значення вектора напряму руху.
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //змінюємо позицію rb, додаючи вектор напряму руху до її координат.
        cam_rb.MovePosition(cam_rb.position + moveInput * cam_speed);
    }
}