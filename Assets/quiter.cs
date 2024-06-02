using UnityEngine;

public class Quitter : MonoBehaviour
{
    public float doubleClickTimeLimit = 0.3f; // Tiempo l�mite para detectar un doble clic (en segundos)
    private float lastClickTime = -1f; // Almacena el tiempo del �ltimo clic

    public GameChanger gameChanger; // Referencia al GameChanger

    void OnMouseDown()
    {
        if (Time.time - lastClickTime < doubleClickTimeLimit)
        {
            // Si el tiempo entre clics es menor al tiempo l�mite, se considera un doble clic
            QuitGame();
        }
        else
        {
            // Almacena el tiempo del clic actual
            lastClickTime = Time.time;
        }
    }

    private void QuitGame()
    {
        if (gameChanger != null)
        {
            gameChanger.QuitGame();
        }
        else
        {
            Debug.LogWarning("GameChanger reference is not set.");
        }
    }
}
