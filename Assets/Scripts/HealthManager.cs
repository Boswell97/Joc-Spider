using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameObject target; // Objeto objetivo con la variable de salud
    public Image healthBar; // Barra de salud que se llenará y vaciará
    private int maxHealth; // Salud máxima del objetivo

    void Start()
    {
        // Obtener la salud máxima del objetivo
        maxHealth = GetMaxHealth(target);

        // Actualizar visualmente la barra de salud al inicio
        UpdateHealthBar();
    }

    // Método para obtener la salud máxima del objetivo
    private int GetMaxHealth(GameObject target)
    {
        if (target.GetComponent<PlayerHealth>() != null)
        {
            return target.GetComponent<PlayerHealth>().maxHealth;
        }
        
        return 0;
    }

    // Método para actualizar visualmente la barra de salud
    private void UpdateHealthBar()
    {
        // Obtener la salud actual del objetivo
        int currentHealth = GetCurrentHealth(target);


        // Calcular el porcentaje de salud actual
        float healthPercentage = (float)currentHealth / maxHealth;

        // Actualizar fillAmount de la barra de salud
        healthBar.fillAmount = healthPercentage;
    }

    // Método para obtener la salud actual del objetivo
    private int GetCurrentHealth(GameObject target)
    {
        if (target.GetComponent<PlayerHealth>() != null)
        {
            return target.GetComponent<PlayerHealth>().currentHealth;
        }
        return -1;
    }

    void Update()
    {
        // Actualizar la barra de salud si ha cambiado la salud del objetivo
        UpdateHealthBar();
    }

    // Método para manejar la muerte del objetivo
    public void HandleDeath()
    {
        // Reiniciar la barra de salud al estado inicial
        healthBar.fillAmount = 1.0f;
    }
}
