using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HP : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    private bool death;    
    [SerializeField] private ProgressBar progressBar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(10);
        }
    }
    public float CurrentHealth
    {
        get => currentHealth;
    }
    public bool Death
    {
        get => death;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckAlive();
        progressBar.ProgressBarChanged(currentHealth, maxHealth);        
    }

    public void CheckAlive()
    {
        if (currentHealth <= 0)
        {
            death = true;
        }
    }

    public void TakeHeal(float heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        progressBar.ProgressBarChanged(currentHealth, maxHealth);
    }
}
