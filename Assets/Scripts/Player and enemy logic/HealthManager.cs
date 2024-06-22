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
        [HideInInspector]
        public HealthConceptEnemy targetHealthEnemy;
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
                healthBarTarget.targetHealthEnemy = healthBarTarget.target.GetComponent<HealthConceptEnemy>();

                if (healthBarTarget.targetHealth != null)
                {
                    healthBarTarget.targetHealth.OnHealthChanged += (healthPercentage) => UpdateHealthBar(healthBarTarget, healthPercentage);
                    UpdateHealthBar(healthBarTarget, (float)healthBarTarget.targetHealth.currentHealth / healthBarTarget.targetHealth.maxHealth);
                }
                else if (healthBarTarget.targetHealthEnemy != null)
                {
                    UpdateHealthBar(healthBarTarget, (float)healthBarTarget.targetHealthEnemy.currentHealth / healthBarTarget.targetHealthEnemy.maxHealth);
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
        if (healthBarTarget.healthBar != null)
        {
            healthBarTarget.healthBar.fillAmount = healthPercentage;
        }
    }

    void Update()
    {
        foreach (var healthBarTarget in healthBarTargets)
        {
            if (healthBarTarget.targetHealthEnemy != null)
            {
                UpdateHealthBar(healthBarTarget, (float)healthBarTarget.targetHealthEnemy.currentHealth / healthBarTarget.targetHealthEnemy.maxHealth);
            }
        }
    }
}
