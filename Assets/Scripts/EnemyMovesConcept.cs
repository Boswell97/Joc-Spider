using UnityEngine;

public class EnemyMovesConcept : MonoBehaviour
{
    public Transform playerTransform; // Player's transform
    public Animator animator; // Reference to the Animator component to control movement animations
    public float moveSpeed = 2f; // Enemy's movement speed
    public string walkAnimationName; // Name of the walk animation

    public bool isAttacking = false; // Indicates if the enemy is attacking

    void Update()
    {
        if (!isAttacking)
        {
            // If the enemy is not attacking, move towards the player
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Move the enemy in the direction of the player
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // If a walk animation is specified, activate it
        if (!string.IsNullOrEmpty(walkAnimationName))
        {
            animator.SetBool(walkAnimationName, true);
        }
    }

    // Method to switch to attack mode
    public void SwitchToAttackMode()
    {
        // Stop movement animation
        if (!string.IsNullOrEmpty(walkAnimationName))
        {
            animator.SetBool(walkAnimationName, false);
        }
    }
}
