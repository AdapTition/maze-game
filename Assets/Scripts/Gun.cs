using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    public float offset = -90; // зміщення кута нахилу від переданих координат, що передані ScreenToWorldPoint() методом 
    public GameObject bullet;
    public Transform shotPoint; // координата місця виліту куль
    private float timeBtwShots;
    public float startTimeBtwShots;
    void Update()
    {
        //зчитування позиції курсору гравця.
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
        
        //обрахунок кута нахилу.
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //нахил.
        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

        //затримка між пострілами та постріл при натисненні лівої клавіші миші.
        if ( timeBtwShots <= 0){
            if (Input.GetMouseButton(0)){
                Instantiate (bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else {
        timeBtwShots -= Time.deltaTime;
        }
    }
}
