using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private float speed = 0.3f;
    private Animator anim;
    private bool key = false;

    [Header ("Health")]
    public int healthCount;
    public Sprite[] healthPNG = new Sprite[5];
    public GameObject HealthImage;

    [Header ("Armour")]
    public int armourCount;
    public Sprite[] armourPNG = new Sprite[5];
    public GameObject ArmourImage;

    [Header ("Bucket")]
    public Sprite[] bucketPNG = new Sprite[5];
    public GameObject BucketImage;

    private GameObject currentTeleporter;


    void Start()
    {
        //запитуємо компонент Rigidbody для майбутньої взаємодії з ним через змінну rb
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        HealthImage.AddComponent(typeof(Image));
        HealthImage.GetComponent<Image>().sprite = healthPNG[healthCount];

        ArmourImage.AddComponent(typeof(Image));
        ArmourImage.GetComponent<Image>().sprite = armourPNG[armourCount];

        this.BucketImage.GetComponent<SpriteRenderer>().sprite = bucketPNG[armourCount];

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
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Potion")){
            if(healthCount < 4){
                ChangeHealth(1);
                Destroy(other.gameObject);
            }
        }
        else if(other.CompareTag("Shield")){
            if(armourCount < 4){
                ChangeArmour(1);
                Destroy(other.gameObject);
                BucketImage.SetActive(true);
            }
        }
        else if(other.CompareTag("Key")){
            key = true;
            Destroy(other.gameObject);
        }

    }
    public void ChangeHealth (int changeValue){
        healthCount += changeValue;
        HealthImage.GetComponent<Image>().sprite = healthPNG[healthCount];
        if (healthCount == 0){
            SceneManager.LoadScene("SampleScene");
        }

    }
    public void ChangeArmour (int changeValue){
        if (armourCount >= changeValue * (-1) ){
            armourCount += changeValue;
        }
        else{
            changeValue -= armourCount;
            armourCount = 0;
            ChangeHealth(changeValue);
        }
        ArmourImage.GetComponent<Image>().sprite = armourPNG[armourCount];
        this.BucketImage.GetComponent<SpriteRenderer>().sprite = bucketPNG[armourCount];
    }

    public bool IsKeyReached(){
        return key;
    }


}