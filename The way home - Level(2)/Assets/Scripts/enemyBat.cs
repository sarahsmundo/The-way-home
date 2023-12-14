using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBat : MonoBehaviour
{

    public bool isFacingRight = false;
    public float maxSpeed = 3f;
    public int damage = 3;
    public float HorizontalSpeed;
    public float VerticalSpeed;
    public float Amplitude;

    private Vector3 tempPosition;

    void Start()
    {
        tempPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Move the enemy in a sine wave pattern
        tempPosition.x += HorizontalSpeed * Time.deltaTime;
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * VerticalSpeed) * Amplitude;

        // Move the enemy
        transform.position = tempPosition;
    }
    //controller class portions

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        FindObjectOfType<PlayerStats>().TakeDamage(damage);
    //    }

    //}
}
