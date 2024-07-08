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
        // Get the Rigidbody2D component, which handles the object's physics
        rb = GetComponent<Rigidbody2D>();
        
        // Get the Animator component for animations
        anim = GetComponent<Animator>();

        // Setting up the sprites for player's health and armor display in the canvas
        HealthImage.AddComponent(typeof(Image));
        HealthImage.GetComponent<Image>().sprite = healthPNG[healthCount];

        ArmourImage.AddComponent(typeof(Image));
        ArmourImage.GetComponent<Image>().sprite = armourPNG[armourCount];

        // Setting up the bucket sprite that is displayed on the player
        this.BucketImage.GetComponent<SpriteRenderer>().sprite = bucketPNG[armourCount];
    }

    void FixedUpdate()
    {
        // Implementing player movement through Rigidbody2D
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.MovePosition(rb.position + moveInput * speed);

        // Enabling the running animation when the player is moving
        if(moveInput.x != 0 || moveInput.y != 0){
            anim.SetBool("isRunning", true);
        }
        else{
            anim.SetBool("isRunning", false);
        }
    }

    // Method to handle triggers the player collides with
    private void OnTriggerEnter2D(Collider2D other){
        // Handling potion collection
        if(other.CompareTag("Potion")){
            if (healthCount < 4){
                ChangeHealth(1);
                Destroy(other.gameObject);
            }
        }
        // Handling shield collection
        else if(other.CompareTag("Shield")){
            if (armourCount < 4){
                ChangeArmour(4);
                Destroy(other.gameObject);
                BucketImage.SetActive(true);
            }
        }
        // Handling key collection
        else if(other.CompareTag("Key")){
            key = true;
            Destroy(other.gameObject);
        }
    }

    // Method to handle changes in player's health
    public void ChangeHealth(int changeValue){
        if (healthCount <= 4){
            healthCount += changeValue;
            if (healthCount <= 0){
                SceneManager.LoadScene("SampleScene");
            }
            HealthImage.GetComponent<Image>().sprite = healthPNG[healthCount];
        }
    }

    // Method to handle changes in player's armor
    public void ChangeArmour(int changeValue){
        if (changeValue > 0){
            armourCount += changeValue;
            if (armourCount > 4) armourCount = 4;
        }
        else if (armourCount >= -changeValue){
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

    // Method to access the key field, necessary for opening the portal doors at the end
    public bool IsKeyReached(){
        return key;
    }
}