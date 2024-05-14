using UnityEngine;

public class VenomAttack : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private float lastAttackTime;
    public float attackCooldown = 2f; // Tiempo de espera entre ataques
    public float attackSpeed = 5f; // Velocidad de ataque
    public int damageAmount = 10; // Cantidad de daño que inflige el ataque
    public float attackRange = 1.5f; // Rango de ataque
    public Transform attackPos; // Posición de ataque

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Attack();
    }

    public void Attack()
    {
        // Si ha pasado el tiempo de espera entre ataques, ejecutar el golpe
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetTrigger("VenomPunch");
            lastAttackTime = Time.time;

            // Hacer daño al jugador si está dentro del rango
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
                if (distanceToPlayer <= attackRange)
                {
                    HealthPlayer playerHealth = player.GetComponent<HealthPlayer>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damageAmount);
                    }
                }
            }
        }

        // Mover al enemigo hacia el jugador a velocidad de ataque
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        float moveSpeed = attackSpeed * Time.fixedDeltaTime;
        rb.position = Vector2.MoveTowards(rb.position, playerPosition, moveSpeed);

        // Voltear el sprite hacia el jugador
        if (playerPosition.x > rb.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
