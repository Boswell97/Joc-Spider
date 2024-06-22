using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClickResumeButton : MonoBehaviour, IPointerClickHandler
{
    public GameChanger gameChanger;
    public float doubleClickTimeLimit = 0.3f;

    private float lastClickTime = -1f;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Button clicked");

        if (Time.time - lastClickTime < doubleClickTimeLimit)
        {
            Debug.Log("Double click detected");
            ResumeGame();
        }
        else
        {
            Debug.Log("Single click, waiting for double click");
            lastClickTime = Time.time;
        }
    }

    private void ResumeGame()
    {
        if (gameChanger != null)
        {
            Debug.Log("GameChanger found");
            if (gameChanger.currentState == GameChanger.GameState.Pause)
            {
                Debug.Log("Resuming game");
                gameChanger.ChangeState(GameChanger.GameState.Playing);
            }
            else
            {
                Debug.Log("Game is not in Pause state");
            }
        }
        else
        {
            Debug.LogError("GameChanger is not assigned");
        }
    }
}
