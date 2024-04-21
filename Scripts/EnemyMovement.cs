using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento del enemigo
    public float jumpForce = 5f; // Fuerza del salto
    public float pushForce = 5f; // Fuerza con la que empuja al jugador
    public LayerMask playerLayer; // Capa del jugador
    public Transform groundCheck; // Punto de chequeo del suelo
    //public Transform leftBoundary; // L�mite izquierdo de la plataforma
    //public Transform rightBoundary; // L�mite derecho de la plataforma
    public Transform enemyAttackPos; // Posici�n del ataque del enemigo
    public float enemyAttackRange; // Rango de ataque del enemigo
    private Animator animator; // Animator del enemigo

    public int maxHealth = 50;
    public int currentHealth;

    //private GameObject currentTeleporter;

    private Rigidbody2D rb;
    private Transform player;
   // private bool isPushing = false;
    private bool isAttacking = false;
    private float lastAttackTime = 0f;
    public float attackCooldown = 2f;
    public int punchDamage = 10; // Da�o causado por el pu�o del enemigo, modificado seg�n tus indicaciones

    private bool isCooldown = false; // Variable para controlar el cooldown
    private float lastAnimationTime = 0f; // Tiempo desde el inicio de la �ltima animaci�n
    public float animationCooldownTime = 5f; // Tiempo de cooldown entre animaciones

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    void Update()
    {
        float direction = (player.position.x - transform.position.x) > 0 ? 1f : -1f;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

       // if (Mathf.Abs(player.position.x - transform.position.x) < 1f && !isPushing)
        //{
          //  isPushing = true;
            //float nearestEdgeX = Mathf.Abs(leftBoundary.position.x - transform.position.x) < Mathf.Abs(rightBoundary.position.x - transform.position.x) ?
              //                  leftBoundary.position.x : rightBoundary.position.x;
            //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * pushForce, 0f), ForceMode2D.Impulse);
        //}
        

        if (!isCooldown)
        {
            if (Time.time >= lastAttackTime + attackCooldown && Vector2.Distance(transform.position, player.position) < enemyAttackRange && !isAttacking)
            {
                isAttacking = true;
                AttackAnimation("enemyPunch"); // Cambio de "enemyRun" a "enemyPunch" para activar la animaci�n de ataque
                Invoke("AttackPlayer", 0.5f); // Invocar el m�todo AttackPlayer despu�s de un breve retraso
                lastAttackTime = Time.time;

                // Registrar el tiempo de inicio de la animaci�n
                lastAnimationTime = Time.time;

                // Activar el cooldown
                isCooldown = true;
            }
            else
            {
                if (!isAttacking)
                {
                    animator.SetTrigger("enemyRun"); // Cambia a la animaci�n de correr mientras busca al jugador
                }
            }
        }
        else
        {
            // Verificar si el cooldown ha terminado
            if (Time.time >= lastAnimationTime + animationCooldownTime)
            {
                isCooldown = false; // Reiniciar el cooldown
            }
        }

        // Flip the enemy sprite based on movement direction
        if (direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // isPushing = false;
            isAttacking = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            // Si la vida del enemigo llega a 0 o menos, destruir el objeto asociado al script
            Destroy(gameObject);
        }
    }

    // M�todo para activar una animaci�n espec�fica
    public void AttackAnimation(string animationName)
    {
        animator.SetTrigger(animationName); // Activar la animaci�n especificada por el nombre
    }

    void AttackPlayer()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(enemyAttackPos.position, enemyAttackRange, playerLayer);
        foreach (Collider2D playerCollider in hitPlayers)
        {
            PlayerHealth player = playerCollider.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(punchDamage); // Causar da�o al jugador
            }
            else { isAttacking = false; }
        }
        isAttacking = false;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyAttackPos.position, enemyAttackRange);
    }

   // private void OnTriggerEnter2D(Collider2D colision) {
     //   if (collision.CompareTag("Teleporter"))
       // {
         //   currentTeleporter = null;
        //}
    //}
    //private void OnTriggerExit2D(Collider2D colision) {
      //  if (colision.CompareTag("Teleporter"))
        //{
          //  if(colision.gameObject== currentTeleporter)
            //{
                //currentTeleporter=null; 
            //}
        //}
    
    
    
    
    //}



}
