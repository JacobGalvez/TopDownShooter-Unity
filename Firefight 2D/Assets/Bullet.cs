using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField] public float bulletDamage;
    
    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Wall":
            Destroy(gameObject);
            break;
            case "Enemy":
            Debug.Log("Enemy Attacked");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
            break;
        }
        Destroy(gameObject, 3f);
    }

    // public void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "Enemy")
    //     {
    //         Debug.Log("Enemy Attacked");
    //         Enemy enemy = other.gameObject.GetComponent<Enemy>();
    //         enemy.TakeDamage(bulletDamage);
    //     }
    //     Destroy(gameObject);
    // }
}
