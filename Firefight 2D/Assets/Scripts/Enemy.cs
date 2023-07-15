using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    [SerializeField] private AudioSource zombieSound;
    [SerializeField] private Slider healthSlider;
    

    private void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        OnGUI();
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
            // Rotate towards the player
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Move towards the player
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
            canAttack = attackSpeed;
            target = other.transform;
            zombieSound.Play();
        }
    }

    private void OnGUI()
    {
        healthSlider.value = health;
    }
}
