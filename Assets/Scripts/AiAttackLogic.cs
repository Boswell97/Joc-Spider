using UnityEngine;

public class AiAttackLogic : MonoBehaviour
{
    public string attackAnimationName; // Nombre de la animación de ataque
    public int damageAmount = 10; // Daño infligido al jugador
    public bool isAttacking = false; // Indica si el enemigo está atacando

    private Animator animator; // Referencia al componente Animator
    private healthConcept playerHealth; // Referencia al script de salud del jugador

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = FindObjectOfType<healthConcept>(); // componente de salud del jugador 

    }

    public void StartAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger(attackAnimationName);
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    // Invocado por la animación de ataque en un evento de animación
    public void DealDamage()
    {
        if (isAttacking && playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}

