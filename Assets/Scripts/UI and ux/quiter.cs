using UnityEngine;
using UnityEngine.EventSystems;

public class Quitter : MonoBehaviour, IPointerClickHandler
{
    public float doubleClickTimeLimit = 0.3f; // Tiempo l�mite para detectar un doble clic (en segundos)
    private float lastClickTime = -1f; // Almacena el tiempo del �ltimo clic

    public GameChanger gameChanger; // Referencia al GameChanger

    // Implementaci�n del m�todo OnPointerClick para detectar clics en UI
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

    // M�todo para salir del juego llamando al m�todo QuitGame del GameChanger
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
