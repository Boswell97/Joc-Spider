using UnityEngine;

public class HealthConceptEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isAlive = true;

    private Animator animator;
    private EnemyDeathHandler deathHandler;
    public BloodLogic bloodLogic;  // Referencia al script BloodLogic

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        deathHandler = GetComponent<EnemyDeathHandler>();

        if (bloodLogic == null)
        {
            bloodLogic = GetComponent<BloodLogic>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive)
        {
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (bloodLogic != null)
        {
            bloodLogic.PlayBloodEffect();  // Llamar al método para reproducir el efecto de sangre
        }

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
