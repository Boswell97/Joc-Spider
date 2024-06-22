using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameChanger : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Pause,
        EndGame
    }

    public GameState currentState;
    public Canvas pauseMenuCanvas;

    void Start()
    {
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.gameObject.SetActive(false);
        }

        currentState = GameState.Playing;
    }

    void Update()
    {
        if (currentState == GameState.Playing && Input.GetKeyDown(KeyCode.P))
        {
            ChangeState(GameState.Pause);
        }
        else if (currentState == GameState.Pause && Input.GetKeyDown(KeyCode.P))
        {
            ChangeState(GameState.Playing);
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        if (currentState == GameState.Pause)
        {
            if (pauseMenuCanvas != null)
            {
                pauseMenuCanvas.gameObject.SetActive(true);
            }
            Time.timeScale = 0;  // Pausar el juego
        }
        else
        {
            if (pauseMenuCanvas != null)
            {
                pauseMenuCanvas.gameObject.SetActive(false);
            }
            Time.timeScale = 1;  // Reanudar el juego
        }

        if (currentState == GameState.EndGame)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("Game has ended.");
        StartCoroutine(WaitForAnyKey());
    }

    IEnumerator WaitForAnyKey()
    {
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void EndGameAndLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void EndGameAndLoadScene(string sceneName, float delay)
    {
        StartCoroutine(WaitAndLoadScene(sceneName, delay));
    }

    IEnumerator WaitAndLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    // Llamar a esta función desde el Quitter para cerrar el programa
    public void QuitGame()
    {
        EndGame();
    }
}
