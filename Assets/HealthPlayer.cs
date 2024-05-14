using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public int maxHealth = 100; // Salud m�xima del objeto
    public int currentHealth; // Salud actual del objeto

    void Start()
    {
        currentHealth = maxHealth; // Establecer la salud inicial al m�ximo al inicio
    }

    // M�todo para que el objeto reciba da�o
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reducir la salud por la cantidad de da�o recibido

        // Comprobar si el objeto est� muerto
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para manejar la muerte del objeto
    void Die()
    {
        // Aqu� puedes agregar cualquier l�gica adicional para cuando el objeto muere
        Destroy(gameObject); // Por ejemplo, puedes destruir el objeto
    }
}

