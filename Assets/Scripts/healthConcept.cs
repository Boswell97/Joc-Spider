using System;
using UnityEngine;

public class healthConcept : MonoBehaviour
{
    public int maxHealth = 100; // Salud m�xima del objetivo

    public int currentHealth; // Salud actual del objetivo
    public int lifes = 2;

    // Evento que se dispara cuando la salud cambia
    public event Action<float> OnHealthChanged;

    void Start()
    {
        // Al inicio, establecer la salud actual como la salud m�xima
        currentHealth = maxHealth;

        // Actualizar visualmente la barra de salud al inicio
        UpdateHealthBar();
    }

    // M�todo para recibir da�o y actualizar la salud
    public void TakeDamage(int damage)
    {
        // Restar el da�o recibido a la salud actual
        currentHealth -= damage;

        // Asegurarse de que la salud no sea menor que cero
        currentHealth = Mathf.Max(currentHealth, 0);

        // Actualizar visualmente la barra de salud
        UpdateHealthBar();

        // Disparar evento de cambio de salud
        float healthPercentage = (float)currentHealth / maxHealth;
        OnHealthChanged?.Invoke(healthPercentage);

        // Verificar si el objetivo ha muerto
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para manejar la muerte del objetivo
    private void Die()
    {
        // Ejecutar acciones espec�ficas de muerte, como desactivar el objeto, reproducir animaciones, etc.
        gameObject.SetActive(false);

        // Si es necesario, puedes agregar m�s acciones aqu�, como reproducir una animaci�n de muerte, etc.
    }

    // M�todo para actualizar visualmente la barra de salud (puede ser manejado por otros scripts)
    private void UpdateHealthBar()
    {
        // Aqu� puedes implementar la l�gica para actualizar visualmente la barra de salud,
        // pero la actualizaci�n real de la barra de salud se manejar� por otros scripts.
        // Por ejemplo, puedes suscribirte al evento OnHealthChanged desde otro script
        // y actualizar la barra de salud all�.
    }
}
