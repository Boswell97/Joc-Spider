using UnityEngine;

public class VenomMoves : MonoBehaviour
{
    public enum State { Patrol, Attack }

    public Transform leftPatrolPoint; // Punto de patrulla izquierdo
    public Transform rightPatrolPoint; // Punto de patrulla derecho
    public float patrolSpeed = 3f; // Velocidad de patrulla
    public float attackSpeed = 5f; // Velocidad de ataque
    public float attackRange = 1.5f; // Rango de ataque
    public float attackCooldown = 2f; // Tiempo de espera entre ataques

    private State currentState = State.Patrol;
    private Animator animator;
    private Rigidbody2D rb;
    private float lastAttackTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ChangeState(State.Patrol);
    }

    void Update()
    {
        // Transiciones entre estados
        if (currentState == State.Patrol)
        {
            // Si el jugador está dentro del rango de ataque, cambiar al estado de ataque
            if (PlayerInRange())
            {
                ChangeState(State.Attack);
            }
        }
        else if (currentState == State.Attack)
        {
            // Si el jugador está fuera del rango de ataque, cambiar al estado de patrulla
            if (!PlayerInRange())
            {
                ChangeState(State.Patrol);
            }
        }
    }

    void FixedUpdate()
    {
        // Ejecutar el comportamiento del estado actual
        if (currentState == State.Patrol)
        {
            Patrol();
        }
        else if (currentState == State.Attack)
        {
            Attack();
        }
    }

    void ChangeState(State newState)
    {
        currentState = newState;
        // Reiniciar la animación en cada cambio de estado
        animator.SetBool("VenomRuns", false);
        animator.SetBool("VenomPunch", false);
        if (currentState == State.Patrol)
        {
            // Configurar la animación de patrulla
            animator.SetBool("VenomRuns", true);
        }
        else if (currentState == State.Attack)
        {
            // Configurar la animación de ataque
            animator.SetBool("VenomPunch", true);
        }
    }


    void Patrol()
    {
        // Verifica que los puntos de patrulla estén asignados
        if (leftPatrolPoint == null || rightPatrolPoint == null)
        {
            Debug.LogWarning("Los puntos de patrulla no están asignados.");
            return;
        }

        // Si está realizando un ataque, no se mueve
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("VenomPunch"))
        {
            return;
        }

        // Determina el punto de destino en función de la posición actual del enemigo
        Vector2 targetPoint = (rb.position.x <= leftPatrolPoint.position.x) ? rightPatrolPoint.position : leftPatrolPoint.position;

        // Calcula la velocidad de movimiento
        float moveSpeed = patrolSpeed * Time.fixedDeltaTime;

        // Mueve al enemigo hacia el punto de destino
        rb.position = Vector2.MoveTowards(rb.position, targetPoint, moveSpeed);

        // Voltea el sprite en la dirección correcta
        if (targetPoint.x > rb.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void Attack()
    {
        // Si ha pasado el tiempo de espera entre ataques, ejecutar el golpe
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetTrigger("VenomPunch");
            lastAttackTime = Time.time;
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

    bool PlayerInRange()
    {
        // Comprobar si el jugador está dentro del rango de ataque
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= attackRange;
        }
        return false;
    }
}
