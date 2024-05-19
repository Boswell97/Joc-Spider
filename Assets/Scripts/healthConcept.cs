using System;
using UnityEngine;

public class healthConcept : MonoBehaviour
{
    public int maxHealth = 100; // Salud máxima del objetivo

    public int currentHealth; // Salud actual del objetivo
    public int lifes = 2;

    // Evento que se dispara cuando la salud cambia
    public event Action<float> OnHealthChanged;

    void Start()
    {
        // Al inicio, establecer la salud actual como la salud máxima
        currentHealth = maxHealth;

        // Actualizar visualmente la barra de salud al inicio
        UpdateHealthBar();
    }

    // Método para recibir daño y actualizar la salud
    public void TakeDamage(int damage)
    {
        // Restar el daño recibido a la salud actual
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

    // Método para manejar la muerte del objetivo
    private void Die()
    {
        // Ejecutar acciones específicas de muerte, como desactivar el objeto, reproducir animaciones, etc.
        gameObject.SetActive(false);

        // Si es necesario, puedes agregar más acciones aquí, como reproducir una animación de muerte, etc.
    }

    // Método para actualizar visualmente la barra de salud (puede ser manejado por otros scripts)
    private void UpdateHealthBar()
    {
        // Aquí puedes implementar la lógica para actualizar visualmente la barra de salud,
        // pero la actualización real de la barra de salud se manejará por otros scripts.
        // Por ejemplo, puedes suscribirte al evento OnHealthChanged desde otro script
        // y actualizar la barra de salud allí.
    }
}
