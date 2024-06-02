using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    public bool ItIsInRange = false;
    private Animator animator;
    public string ItIsInRangeParam;
    public string IsPunchingParam;

    private AiAttackLogic attackLogic;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInParent<Animator>();
        }

        attackLogic = GetComponentInParent<AiAttackLogic>();

        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on this GameObject or its parent.");
        }

        if (attackLogic == null)
        {
            Debug.LogWarning("AiAttackLogic component not found on the parent GameObject.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItIsInRange = true;
            UpdateAnimator();
            attackLogic.StartAttack();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItIsInRange = false;
            UpdateAnimator();
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
}

