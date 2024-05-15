using UnityEngine;

public class EnemyAttackConcept : MonoBehaviour
{
    public Transform playerTransform; // Transformador del jugador
    public Animator animator; // Referencia al componente Animator para controlar las animaciones de ataque
    public Transform attackRange; // Transformador que define el rango de ataque del enemigo
    public string attackAnimationName; // Nombre de la animación de ataque

    private EnemyMovesConcept enemyMoves; // Referencia al script de movimiento del enemigo

    void Start()
    {
        // Obtener la referencia al script de movimiento del enemigo
        enemyMoves = GetComponent<EnemyMovesConcept>();
    }

    void Update()
    {
        // Si el enemigo está en modo de ataque, realizar el ataque
        if (enemyMoves.isAttacking)
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        // Detectar si el jugador está dentro del rango de ataque
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange.localScale.x / 2)
        {
            // Si se ha especificado una animación de ataque, activarla
            if (!string.IsNullOrEmpty(attackAnimationName))
            {
                animator.SetTrigger(attackAnimationName);
            }

            // Cambiar al estado de movimiento después de atacar
            enemyMoves.isAttacking = false;
            enemyMoves.SwitchToAttackMode();
        }
    }
}
