using UnityEngine;
using UnityEngine.EventSystems;

public class Quitter : MonoBehaviour, IPointerClickHandler
{
    public float doubleClickTimeLimit = 0.3f; // Tiempo límite para detectar un doble clic (en segundos)
    private float lastClickTime = -1f; // Almacena el tiempo del último clic

    public GameChanger gameChanger; // Referencia al GameChanger

    // Implementación del método OnPointerClick para detectar clics en UI
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Button clicked");

        if (Time.time - lastClickTime < doubleClickTimeLimit)
        {
            Debug.Log("Double click detected");
            QuitGame();
        }
        else
        {
            Debug.Log("Single click, waiting for double click");
            lastClickTime = Time.time;
        }
    }

    // Método para salir del juego llamando al método QuitGame del GameChanger
    private void QuitGame()
    {
        if (gameChanger != null)
        {
            Debug.Log("Calling QuitGame on GameChanger");
            gameChanger.QuitGame();
        }
        else
        {
            Debug.LogWarning("GameChanger reference is not set.");
        }
    }
}
