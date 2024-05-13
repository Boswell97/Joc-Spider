using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animatorPlayer;
    public SpriteRenderer srPlayer;
    public float punchForce = 500.0f;
    public int punchDamage = 1;
    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    private bool isPunchPressed = false;
    private bool isGrounded = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isPunchPressed = true;
            animatorPlayer.SetBool("isPunching", true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            animatorPlayer.SetBool("isKick", true);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animatorPlayer.SetBool("isShooting", true);
        }

        if (Input.GetKeyUp(KeyCode.Space)) { Invoke("ResetAnimation", 0.3f); }
        if (Input.GetKeyUp(KeyCode.K)) { Invoke("ResetAnimation", 0.3f); }
        if (Input.GetKeyUp(KeyCode.F)) { Invoke("ResetAnimation", 0.3f); }
    }

    void FixedUpdate()
    {
        if (isPunchPressed)
        {
            Punch();
            isPunchPressed = false;
        }
    }

    void ResetAnimation()
    {
        animatorPlayer.SetBool("isPunching", false);
        animatorPlayer.SetBool("isKick", false);
        animatorPlayer.SetBool("isShooting", false);
    }

    public void Punch()
    {
        Vector2 punchDirection = srPlayer.flipX ? Vector2.left : Vector2.right;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        foreach (Collider2D enemyCollider in enemiesToDamage)
        {
            VenomHealth enemyHealth = enemyCollider.GetComponent<VenomHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(punchDamage); // Aplicar daño al enemigo
            }

            Rigidbody2D enemyRb = enemyCollider.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                enemyRb.AddForce(punchDirection * punchForce);
            }
        }
    }

    public void Kick()
    {
        Vector2 kickDirection = srPlayer.flipX ? Vector2.left : Vector2.right;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        foreach (Collider2D enemyCollider in enemiesToDamage)
        {
            VenomHealth enemyHealth = enemyCollider.GetComponent<VenomHealth>();
            if (enemyHealth != null)
            {
                //enemyHealth.TakeDamage(kickDamage);
            }

            Rigidbody2D enemyRb = enemyCollider.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                enemyRb.AddForce(kickDirection * punchForce);
            }
        }
    }

    public void Shoot()
    {
        Vector2 shootDirection = srPlayer.flipX ? Vector2.left : Vector2.right;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        foreach (Collider2D enemyCollider in enemiesToDamage)
        {
            VenomHealth enemyHealth = enemyCollider.GetComponent<VenomHealth>();
            if (enemyHealth != null)
            {
                //enemyHealth.TakeDamage(punchDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
