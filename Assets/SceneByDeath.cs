using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneByDeath : MonoBehaviour
{
    public healthConcept healthScript;
    public string sceneToLoad;
    public bool delayBeforeSceneChange = false;
    public float delayTime = 6f;
    private bool isChangingScene = false;

    void Start()
    {
        if (healthScript == null)
        {
            healthScript = GetComponent<healthConcept>();
        }

        if (healthScript != null)
        {
            healthScript.OnHealthChanged += HandleHealthChanged;
        }
        else
        {
            Debug.LogError("HealthConcept script not found on the GameObject.");
        }
    }

    void HandleHealthChanged(float healthPercentage)
    {
        if (!isChangingScene && healthPercentage <= 0)
        {
            isChangingScene = true;
            if (delayBeforeSceneChange)
            {
                Invoke("LoadScene", delayTime);
            }
            else
            {
                LoadScene();
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnDestroy()
    {
        if (healthScript != null)
        {
            healthScript.OnHealthChanged -= HandleHealthChanged;
        }
    }
}
