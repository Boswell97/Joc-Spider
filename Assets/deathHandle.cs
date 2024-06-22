using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathHandle : MonoBehaviour
{
    public GameObject target;  // El objetivo que representa la muerte
    public string sceneToLoad; // La escena a cargar al morir este objetivo
    public bool delayBeforeSceneChange = false; // Si hay un retraso antes de cambiar de escena
    public float delayTime = 6f; // Tiempo de retraso antes de cambiar de escena

    private GameChanger gameChanger;

    void Start()
    {
        gameChanger = FindObjectOfType<GameChanger>();
        if (gameChanger == null)
        {
            Debug.LogError("GameChanger not found in the scene.");
        }
    }

    void OnEnable()
    {
        if (gameChanger != null)
        {
            if (delayBeforeSceneChange)
            {
                gameChanger.EndGameAndLoadScene(sceneToLoad, delayTime);
            }
            else
            {
                gameChanger.EndGameAndLoadScene(sceneToLoad);
            }
        }
    }
}
