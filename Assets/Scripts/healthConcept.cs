using System;
using UnityEngine;

public class healthConcept : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int lifes = 2;
    public event Action<float> OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        //UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        //UpdateHealthBar();
        float healthPercentage = (float)currentHealth / maxHealth;
        OnHealthChanged?.Invoke(healthPercentage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        lifes--;

        if (lifes > 0)
        {
            currentHealth = maxHealth;
          //  UpdateHealthBar();
            float healthPercentage = (float)currentHealth / maxHealth;
            OnHealthChanged?.Invoke(healthPercentage);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        //UpdateHealthBar();
        float healthPercentage = (float)currentHealth / maxHealth;
        OnHealthChanged?.Invoke(healthPercentage);
    }
}
