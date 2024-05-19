using UnityEngine;

public class HealthConceptEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isAlive = true;

    private Animator animator;
    private EnemyDeathHandler deathHandler;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        deathHandler = GetComponent<EnemyDeathHandler>();//logica de muerte 
    }
    
    public void TakeDamage(int damage)
    {
        if (!isAlive)
        {
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;

        if (deathHandler != null)
        {
            deathHandler.HandleDeath();
        }
    }
}
