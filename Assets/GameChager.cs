using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChanger : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Pause,
        EndGame
    }
    IEnumerator WaitAndLoadScene(string sceneName)
    {
        // no me funes pls no me acuerdo de como se hacia el otro,solo lo hago para que se vea la animacion
        yield return new WaitForSeconds(6f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public GameState currentState;
    public Canvas pauseMenuCanvas;
    public string nameOfSceneDeath;

    public List<GameObject> targets; 

    /// <summary>
    /// cambia las list de abajo por lo scripts de vida correspodientes para reciclar 
    /// </summary>
    
    public List<healthConcept> targetsHealth = new List<healthConcept>(); 
    public List<HealthConceptEnemy> targetsHealthEnemy = new List<HealthConceptEnemy>(); 

    void Start()
    {
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.gameObject.SetActive(false);
        }

        currentState = GameState.Playing;

        // Inicializar los componentes de salud para cada target
        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                healthConcept targetHealth = target.GetComponent<healthConcept>();
                if (targetHealth != null)
                {
                    targetsHealth.Add(targetHealth);
                    targetHealth.OnHealthChanged += CheckTargetsHealth;
                }
                else
                {
                    HealthConceptEnemy targetHealthEnemy = target.GetComponent<HealthConceptEnemy>();
                    if (targetHealthEnemy != null)
                    {
                        targetsHealthEnemy.Add(targetHealthEnemy);
                        
                    }
                }
            }
        }
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
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void EndGameAndLoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    void CheckTargetsHealth(float healthPercentage)
    {
        foreach (healthConcept targetHealth in targetsHealth)
        {
            if (targetHealth != null && targetHealth.currentHealth <= 0)
            {
                EndGameAndLoadScene(nameOfSceneDeath);
                return; // Salir del bucle una vez que se encuentra un target muerto
            }
        }

        foreach (HealthConceptEnemy targetHealthEnemy in targetsHealthEnemy)
        {
            if (targetHealthEnemy != null && targetHealthEnemy.currentHealth <= 0)
            {
                StartCoroutine(WaitAndLoadScene(nameOfSceneDeath));
                return; // Salir del bucle una vez que se encuentra un target enemigo muerto
            }
        }
    }

    // Llamar a esta función desde el Quitter para cerrar el programa
    public void QuitGame()
    {
        EndGame();
    }
}
