using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [System.Serializable]
    public class HealthBarTarget
    {
        public GameObject target;
        public Image healthBar;
        [HideInInspector]
        public healthConcept targetHealth;
    }

    [Header("Health Bar Targets")]
    public List<HealthBarTarget> healthBarTargets = new List<HealthBarTarget>();

    void Start()
    {
        foreach (var healthBarTarget in healthBarTargets)
        {
            if (healthBarTarget.target != null)
            {
                healthBarTarget.targetHealth = healthBarTarget.target.GetComponent<healthConcept>();
                if (healthBarTarget.targetHealth != null)
                {
                    healthBarTarget.targetHealth.OnHealthChanged += (healthPercentage) => UpdateHealthBar(healthBarTarget, healthPercentage);
                }
                else
                {
                    Debug.LogError("No se encontró el componente Health en el objetivo " + healthBarTarget.target.name);
                }
            }
            else
            {
                Debug.LogError("Target no asignado en uno de los elementos de healthBarTargets.");
            }

            UpdateHealthBar(healthBarTarget, (float)healthBarTarget.targetHealth.currentHealth / healthBarTarget.targetHealth.maxHealth);
        }
    }

    void OnDestroy()
    {
        foreach (var healthBarTarget in healthBarTargets)
        {
            if (healthBarTarget.targetHealth != null)
            {
                healthBarTarget.targetHealth.OnHealthChanged -= (healthPercentage) => UpdateHealthBar(healthBarTarget, healthPercentage);
            }
        }
    }

    private void UpdateHealthBar(HealthBarTarget healthBarTarget, float healthPercentage)
    {
        if (healthBarTarget.targetHealth != null)
        {
            healthBarTarget.healthBar.fillAmount = healthPercentage;
        }
    }

    void Update()
    {
       
    }
}
