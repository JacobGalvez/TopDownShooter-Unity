using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Transform target;
    [SerializeField] private float attackDamage = 1f;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;

    [Header("Health")]
    [Header("Attack")]

    private float health;
    [SerializeField] private float maxHealth;


    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log("Enemy health " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(attackSpeed <= canAttack) 
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;

        }
    }

    // IF PLAYER LEAVES AGGRO RADIUS THEY WILL STOP CHASING - DISABLED FOR SURVIVAL MODE
    // private void OnTriggerExit2D(Collider2D other) 
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         target = null;
    //     }
    // }
}
