using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameObject target; // Objeto objetivo con el componente de salud
    public Image healthBar; // Barra de salud que se llenará y vaciará
    public healthConcept targetHealth; // Referencia al componente Health del objetivo

    void Start()
    {
        // Obtener el componente de salud del objetivo
        targetHealth = target.GetComponent<healthConcept>();

        // Actualizar visualmente la barra de salud al inicio
        UpdateHealthBar();
    }

    // Método para actualizar visualmente la barra de salud
    private void UpdateHealthBar()
    {
        if (targetHealth != null)
        {
            // Calcular el porcentaje de salud actual
            float healthPercentage = (float)targetHealth.currentHealth / targetHealth.maxHealth;

            // Actualizar fillAmount de la barra de salud
            healthBar.fillAmount = healthPercentage;
        }
        else
        {
            Debug.LogError("No se encontró el componente Health en el objetivo.");
        }
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
