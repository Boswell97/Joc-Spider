using UnityEngine;

public class HealthConceptEnemy : MonoBehaviour
{
    public int maxHealth = 100; // Salud m�xima del enemigo
    private int currentHealth; // Salud actual del enemigo

    void Start()
    {
        // Al inicio, establecer la salud actual como la salud m�xima
        currentHealth = maxHealth;
    }

    // M�todo para recibir da�o y actualizar la salud
    public void TakeDamage(int damage)
    {
        // Restar el da�o recibido a la salud actual
        currentHealth -= damage;

        // Asegurarse de que la salud no sea menor que cero
        currentHealth = Mathf.Max(currentHealth, 0);

        // Verificar si el enemigo ha muerto
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para manejar la muerte del enemigo
    private void Die()
    {
        // Ejecutar acciones espec�ficas de muerte, como desactivar el objeto, reproducir animaciones, etc.
        gameObject.SetActive(false);

        // Si es necesario, puedes agregar m�s acciones aqu�, como incrementar la puntuaci�n del jugador, etc.
    }
}
