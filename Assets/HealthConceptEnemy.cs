using UnityEngine;

public class HealthConceptEnemy : MonoBehaviour
{
    public int maxHealth = 100; // Salud máxima del enemigo
    private int currentHealth; // Salud actual del enemigo

    void Start()
    {
        // Al inicio, establecer la salud actual como la salud máxima
        currentHealth = maxHealth;
    }

    // Método para recibir daño y actualizar la salud
    public void TakeDamage(int damage)
    {
        // Restar el daño recibido a la salud actual
        currentHealth -= damage;

        // Asegurarse de que la salud no sea menor que cero
        currentHealth = Mathf.Max(currentHealth, 0);

        // Verificar si el enemigo ha muerto
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para manejar la muerte del enemigo
    private void Die()
    {
        // Ejecutar acciones específicas de muerte, como desactivar el objeto, reproducir animaciones, etc.
        gameObject.SetActive(false);

        // Si es necesario, puedes agregar más acciones aquí, como incrementar la puntuación del jugador, etc.
    }
}
