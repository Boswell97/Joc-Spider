using UnityEngine;
using UnityEngine.UI;

public class HealthConcept : MonoBehaviour
{
    public int maxHealth = 100; // Salud m�xima del objetivo
    public Image healthBar; // Barra de salud que se llenar� y vaciar�
    public int lifes = 2;
    private int currentHealth; // Salud actual del objetivo

    void Start()
    {
        // Al inicio, establecer la salud actual como la salud m�xima
        currentHealth = maxHealth;

        // Actualizar visualmente la barra de salud al inicio
        //UpdateHealthBar();
    }

    // M�todo para recibir da�o y actualizar la salud
    public void TakeDamage(int damage)
    {
        // Restar el da�o recibido a la salud actual
        currentHealth -= damage;

        // Asegurarse de que la salud no sea menor que cero
        currentHealth = Mathf.Max(currentHealth, 0);

        // Actualizar visualmente la barra de salud
        //UpdateHealthBar();

        // Verificar si el objetivo ha muerto
        if (currentHealth <= 0)
        {
            lifes--;
            currentHealth = maxHealth;

        }
        if (currentHealth <= 0&&lifes<1) {
            
            
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
}
