using UnityEngine;

public class AttackConcept : MonoBehaviour
{
    private float timeBtwAttack;
    public float starTimeBtwAttack;
    public Animator animator;
    public LayerMask Enemylayer;
   public Transform attackRange; 
    public int damageAmount = 5; 
    public string attackAnimationName;
    public float attackRadius;

    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                if (!string.IsNullOrEmpty(attackAnimationName))
                {
                    animator.SetTrigger(attackAnimationName);
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackRange.position, attackRadius, Enemylayer);
                    PerformAttack(enemiesToDamage);
                }

                //  the attack
                timeBtwAttack = starTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            // desactiva el true
            if (!string.IsNullOrEmpty(attackAnimationName))
            {
                animator.ResetTrigger(attackAnimationName);
            }
        }
    }

    void PerformAttack(Collider2D[] targets)
    {
        // el daño en si(mientras tenga algun componente health el enemig 
        foreach (Collider2D target in targets)
        {
            HealthConceptEnemy health = target.GetComponent<HealthConceptEnemy>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }

    
}
