using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeathHandler : MonoBehaviour
{
    public string deathTriggerParameter = "Die";
    public GameObject[] objectsToFreeze;
    public string sceneToLoad; // Nombre de la escena a cargar antes de destruir el objeto
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleDeath()
    {
        if (animator != null)
        {
            animator.SetTrigger(deathTriggerParameter);
        }

        foreach (var obj in objectsToFreeze)
        {
            if (obj != null)
            {
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                }
            }
        }

        // Esperar antes de cargar la escena y destruir el objeto
        Invoke("LoadSceneAndDestroy", 12f);
    }

    void LoadSceneAndDestroy()
    {
        // Cargar la escena especificada
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        // Destruir el objeto después de cargar la escena
        Destroy(gameObject);
    }
}
