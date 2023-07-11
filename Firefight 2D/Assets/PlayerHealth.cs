using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health = 0f;
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private Slider healthSlider;

    private void Start() {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    public void UpdateHealth(float mod) 
    {
        health += mod;
        
        if(health > maxHealth) 
        {
            health = maxHealth;
        }
        else if (health <= 0)
        {
            health = 0f;
            Debug.Log("Player Respawn");
        }
    }

    private void OnGUI() 
    {
        healthSlider.value = health;
    }
}
