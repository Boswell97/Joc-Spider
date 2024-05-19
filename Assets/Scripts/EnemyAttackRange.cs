using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    public bool ItIsInRange = false;
    private Animator animator;
    public Transform elPlayer;
    public Transform elEnemy;
    public PatrolAi fliper;
    public string ItIsInRangeParam;
    public string IsPunchingParam;

    private AiAttackLogic attackLogic;

    private void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
        fliper = transform.parent.GetComponent<PatrolAi>();
        attackLogic = transform.parent.GetComponent<AiAttackLogic>();

        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on parent GameObject.");
        }

        if (attackLogic == null)
        {
            Debug.LogWarning("AiAttackLogic component not found on parent GameObject.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItIsInRange = true;
            UpdateAnimator();
            FlipTowardsPlayer(other.transform);
            attackLogic.StartAttack();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItIsInRange = false;
            UpdateAnimator();
            FlipTowardsPatrolPoint();
            attackLogic.EndAttack();
        }
    }

    private void UpdateAnimator()
    {
        if (animator != null)
        {
            animator.SetBool(ItIsInRangeParam, ItIsInRange);
            if (!ItIsInRange)
            {
                animator.SetBool(IsPunchingParam, false);
            }
        }
    }

    private void FlipTowardsPlayer(Transform playerTransform)
    {
        if (elEnemy != null)
        {
            Vector3 direction = playerTransform.position - elEnemy.position;
            if (direction.x < 0 && elEnemy.localScale.x > 0 || direction.x > 0 && elEnemy.localScale.x < 0)
            {
                Vector3 escala = elEnemy.localScale;
                escala.x *= -1;
                elEnemy.localScale = escala;
            }
        }
    }

    private void FlipTowardsPatrolPoint()
    {
        if (elEnemy != null && fliper != null)
        {
            Transform nextPatrolPoint = fliper.puntoActual;
            Vector3 direction = nextPatrolPoint.position - elEnemy.position;

            if (direction.x < 0 && elEnemy.localScale.x > 0 || direction.x > 0 && elEnemy.localScale.x < 0)
            {
                Vector3 escala = elEnemy.localScale;
                escala.x *= -1;
                elEnemy.localScale = escala;
            }
        }
    }
}

