using UnityEngine;

public class VenomHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salud m�xima del enemigo
    private int currentHealth; // Salud actual del enemigo

    void Start()
    {
        currentHealth = maxHealth; // Inicializar la salud actual con el valor m�ximo
    }

    // Funci�n para recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reducir la salud actual por el valor de da�o

        // Verificar si la salud llega a cero o menos
        if (currentHealth <= 0)
        {
            Die(); // Llamar a la funci�n para la muerte del enemigo
        }
    }

    // Funci�n para la muerte del enemigo
    void Die()
    {
      //animacion muerte puto 
        Destroy(gameObject); // Por ahora simplemente destruimos el GameObject del enemigo
    }
}
