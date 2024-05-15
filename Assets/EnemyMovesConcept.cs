using UnityEngine;

public class EnemyMovesConcept : MonoBehaviour
{
    public Transform playerTransform; // Transformador del jugador
    public Animator animator; // Referencia al componente Animator para controlar las animaciones de movimiento
    public float moveSpeed = 2f; // Velocidad de movimiento del enemigo
    public string walkAnimationName; // Nombre de la animaci�n de caminar

    public bool isAttacking = false; // Indica si el enemigo est� atacando

    void Update()
    {
        if (!isAttacking)
        {
            // Si el enemigo no est� atacando, moverse hacia el jugador
            MoveTowardsPlayer();
        }
    }

    public void MoveTowardsPlayer()
    {
        // Calcular la direcci�n hacia el jugador
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Mover al enemigo en la direcci�n del jugador
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Si se ha especificado una animaci�n de caminar, activarla
        if (!string.IsNullOrEmpty(walkAnimationName))
        {
            animator.SetBool(walkAnimationName, true);
        }
    }

    // M�todo para cambiar al estado de ataque
    public void SwitchToAttackMode()
    {
        isAttacking = true;

        // Si se ha especificado una animaci�n de caminar, desactivarla
        if (!string.IsNullOrEmpty(walkAnimationName))
        {
            animator.SetBool(walkAnimationName, false);
        }
    }
}

