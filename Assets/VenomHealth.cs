using UnityEngine;

public class VenomHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salud máxima del enemigo
    private int currentHealth; // Salud actual del enemigo

    void Start()
    {
        currentHealth = maxHealth; // Inicializar la salud actual con el valor máximo
    }

    // Función para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reducir la salud actual por el valor de daño

        // Verificar si la salud llega a cero o menos
        if (currentHealth <= 0)
        {
            Die(); // Llamar a la función para la muerte del enemigo
        }
    }

    // Función para la muerte del enemigo
    void Die()
    {
      //animacion muerte puto 
        Destroy(gameObject); // Por ahora simplemente destruimos el GameObject del enemigo
    }
}
