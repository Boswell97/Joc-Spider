using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resumer : MonoBehaviour
{
    public float doubleClickTimeLimit = 0.3f; 
    private float lastClickTime = -1f;
    public GameChanger gameChanger; 

    void Start()
    {
        if (gameChanger == null)
        {
            Debug.LogWarning("GameChanger reference is not set.");
        }
    }

    void OnMouseDown()
    {
        if (gameChanger != null && Time.time - lastClickTime < doubleClickTimeLimit)
        {
            gameChanger.ChangeState(GameChanger.GameState.Playing);
        }
        else
        {
            // Almacena el tiempo del clic actual
            lastClickTime = Time.time;
        }
    }
}
