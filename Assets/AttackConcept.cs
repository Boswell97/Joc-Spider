using UnityEngine;

public class AttackConcept : MonoBehaviour
{
    public Animator animator; // Referencia al componente Animator para controlar las animaciones
    public LayerMask targetLayer; // Capa de los objetivos a los que se puede atacar
    public Transform attackRange; // Transformador que define el rango de ataque
    public int damageAmount = 5; // Da�o infligido por el ataque
    public string attackAnimationName; // Nombre de la animaci�n de ataque a activar

    void Update()
    {
        // Detectar si se quiere atacar (por ejemplo, al presionar un bot�n)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Activar la animaci�n de ataque especificada
            if (!string.IsNullOrEmpty(attackAnimationName))
            {
                animator.SetTrigger(attackAnimationName);
            }

            // Realizar el ataque
            PerformAttack();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            // Desactivar la animaci�n de ataque cuando se deja de presionar la tecla
            if (!string.IsNullOrEmpty(attackAnimationName))
            {
                animator.ResetTrigger(attackAnimationName);
            }
        }
    }

    void PerformAttack()
    {
        // Detectar objetivos dentro del rango de ataque
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackRange.position, attackRange.localScale.x / 2, targetLayer);

        // Aplicar da�o a los objetivos detectados
        foreach (Collider2D target in hitTargets)
        {
            HealthConcept health = target.GetComponent<HealthConcept>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }
}
