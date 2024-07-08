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
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Нашалтування відображення спрайтів кількості здоров'я та броні гравця в canvas.
        HealthImage.AddComponent(typeof(Image));
        HealthImage.GetComponent<Image>().sprite = healthPNG[healthCount];

        ArmourImage.AddComponent(typeof(Image));
        ArmourImage.GetComponent<Image>().sprite = armourPNG[armourCount];

        this.BucketImage.GetComponent<SpriteRenderer>().sprite = bucketPNG[armourCount];

    }


    void FixedUpdate()
    {
        //реалізація руху rb гравця.
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.MovePosition(rb.position + moveInput * speed);

        //ввімкнення анімації руху, коли гравець рухається.
        if(moveInput.x != 0 || moveInput.y != 0  ){
            anim.SetBool("isRunning", true);
        }
        else{
            anim.SetBool("isRunning", false);
        }
    }

    // метод для обробки тригерів, що гравець зачевив.
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Potion")){
            if (healthCount < 4){
                ChangeHealth(1);
                Destroy(other.gameObject);
            }
        }
        else if(other.CompareTag("Shield")){
            if (armourCount < 4){
                ChangeArmour(4);
                Destroy(other.gameObject);
                BucketImage.SetActive(true);
            }
        }
        else if(other.CompareTag("Key")){
            key = true;
            Destroy(other.gameObject);
        }

    }

  //метод обробки змін здоров'я гравця.
    public void ChangeHealth (int changeValue){
        if (healthCount <= 4){
            healthCount += changeValue;
        if (healthCount <= 0){
                SceneManager.LoadScene("SampleScene");
            }
            HealthImage.GetComponent<Image>().sprite = healthPNG[healthCount];
        }

    }

    //метод обробки змін броні гравця.
    public void ChangeArmour (int changeValue){
        
        if (changeValue > 0 ){
            armourCount += changeValue;
            if (armourCount > 4) armourCount = 4;
        }
        else if ( armourCount >= -changeValue){
            armourCount += changeValue;
        }
        else{
            changeValue += armourCount;
            armourCount = 0;
            ChangeHealth(changeValue);
        }
        ArmourImage.GetComponent<Image>().sprite = armourPNG[armourCount];
        this.BucketImage.GetComponent<SpriteRenderer>().sprite = bucketPNG[armourCount];
    }

    //метод доступу до поля key. необхідне для відкриття припортальних дверей в кінці.
    public bool IsKeyReached(){
        return key;
    }


}