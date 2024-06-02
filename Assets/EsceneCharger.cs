using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCharger : MonoBehaviour
{
    public string sceneName; 
    public GameChanger gameChanger; 
    void OnMouseDown()
    {
       
        if (!string.IsNullOrEmpty(sceneName))
        {
            if (gameChanger != null)
            {
                
                gameChanger.EndGameAndLoadScene(sceneName);
            }
            else
            {
               
                SceneManager.LoadScene(sceneName);
            }
        }
        else
        {
            Debug.LogError("El nombre de la escena no está asignado.");
        }
    }
}
