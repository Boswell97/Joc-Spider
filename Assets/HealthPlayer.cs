using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public int maxHealth = 100; // Salud máxima del objeto
    public int currentHealth; // Salud actual del objeto

    void Start()
    {
        currentHealth = maxHealth; // Establecer la salud inicial al máximo al inicio
    }

    // Método para que el objeto reciba daño
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reducir la salud por la cantidad de daño recibido

        // Comprobar si el objeto está muerto
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para manejar la muerte del objeto
    void Die()
    {
        // Aquí puedes agregar cualquier lógica adicional para cuando el objeto muere
        Destroy(gameObject); // Por ejemplo, puedes destruir el objeto
    }
}

