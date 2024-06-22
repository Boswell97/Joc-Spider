using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EsceneCharger : MonoBehaviour
{

    public string sceneName;
    public GameChanger gameChanger;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(HandleButtonClick);
        }
        else
        {
            Debug.LogError("No se encontró el componente Button en el GameObject asociado.");
        }
    }

    void HandleButtonClick()
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

    public void SetSceneName(string newName)
    {
        sceneName = newName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandleButtonClick();
    }
}
