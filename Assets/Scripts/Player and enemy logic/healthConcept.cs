using System;
using UnityEngine;

public class healthConcept : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int lifes = 2;
    public event Action<float> OnHealthChanged;
    public BloodLogic bloodLogic;  // Referencia al script BloodLogic

    void Start()
    {
        currentHealth = maxHealth;
        if (bloodLogic == null)
        {
            bloodLogic = GetComponent<BloodLogic>();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (bloodLogic != null)
        {
            bloodLogic.PlayBloodEffect();  // Llamar al método para reproducir el efecto de sangre
        }

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
        float healthPercentage = (float)currentHealth / maxHealth;
        OnHealthChanged?.Invoke(healthPercentage);
    }
}
