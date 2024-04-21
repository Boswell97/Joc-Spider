using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animatorPlayer;
    public SpriteRenderer srPlayer;
    public float punchForce = 500.0f;
    public int punchDamage = 1;
    private bool isPunchPressed = false;
    private bool isPunching = false;
    private float lastPunchTime = 0f;
    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float attackRange;

    void Update()
    {
        // Detecta si se presiona la tecla de golpear
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPunchPressed = true;
        }

        animatorPlayer.SetBool("isPunching", isPunching);
    }

    void FixedUpdate()
    {
        // Realiza un golpe si se ha presionado la tecla y ha pasado el tiempo de enfriamiento
        if (isPunchPressed && !isPunching && Time.time >= lastPunchTime)
        {
            Punch();
            isPunchPressed = false;
            lastPunchTime = Time.time;
        }
    }

    // Restablece el estado de golpeo del jugador
    private void EndPunch()
    {
        isPunching = false;
    }

    // Realiza un golpe
    public void Punch()
    {
        isPunching = true;
        Vector2 punchDirection = srPlayer.flipX ? Vector2.left : Vector2.right;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        foreach (Collider2D enemyCollider in enemiesToDamage)
        {
            // Hacer daño al enemigo
            EnemyMovement enemyHealth = enemyCollider.GetComponent<EnemyMovement>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(punchDamage);
            }

            Rigidbody2D enemyRb = enemyCollider.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                enemyRb.AddForce(punchDirection * punchForce);
            }
        }

        Invoke("EndPunch", 0.5f);
    }

    // Dibuja un gizmo para el área de ataque
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
