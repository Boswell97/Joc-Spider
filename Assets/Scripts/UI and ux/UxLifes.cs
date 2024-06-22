using UnityEngine;
using TMPro;

public class UxLifes : MonoBehaviour
{
    public Transform playerTransform; // Referencia al objeto que contiene los datos del jugador
    public TextMeshProUGUI lifesText; // Referencia al TextMeshProUGUI para mostrar las vidas

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null || lifesText == null)
        {
            Debug.LogWarning("Player Transform or Lifes Text not assigned in UxLifes script.");
            return;
        }

        // Obtener el componente PlayerMovement del objeto del jugador
        healthConcept dios = playerTransform.GetComponent<healthConcept>();

        // Verificar si se encontró el componente PlayerMovement y si tiene una variable lifes
        if (dios != null)
        {
             int lifes = dios.lifes; // Obtener el valor de lifes del script del jugador

            // Actualizar el texto de las vidas con el valor actual del jugador
            lifesText.text = "Lifes: " + lifes.ToString();
        }
        else
        {
            Debug.LogWarning("PlayerMovement component not found on player object.");
        }
    }
}
