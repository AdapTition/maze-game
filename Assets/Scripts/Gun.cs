using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    // Offset angle from the coordinates provided by ScreenToWorldPoint() method
    public float offset = -90;

    // Prefab of the bullet to be instantiated
    public GameObject bullet;

    // The position where the bullet will be shot from
    public Transform shotPoint;

    // Time between shot
    private float timeBtwShots;
    public float startTimeBtwShots;

    void Update()
    {
        // Read the player's cursor position
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 

        // Calculate the angle of rotation
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Apply the rotation with an offset
        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

        // Handle shooting delay and shooting when the left mouse button is pressed
        if (timeBtwShots <= 0){
            if (Input.GetMouseButton(0)){
                // Instantiate the bullet at the shot point with the current rotation
                Instantiate(bullet, shotPoint.position, transform.rotation);
                // Reset the time between shots
                timeBtwShots = startTimeBtwShots;
            }
        }
        else {
            // Decrease the time between shots timer
            timeBtwShots -= Time.deltaTime;
        }
    }
}