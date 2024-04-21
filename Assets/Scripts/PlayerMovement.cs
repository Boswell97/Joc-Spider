using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbplayer;
    public float speedPlayer = 3.0f;
    public float walkingSpeed = 3.0f;
    public float crouchingSpeed = 1.95f; // Reducir la velocidad a un cuarto al agacharse
    public float acceleration = 5.0f; // Aceleración al correr
    public float maxRunningSpeed = 7.5f; // Velocidad máxima al correr
    public Animator animatorPlayer;
    public SpriteRenderer srPlayer;

    private float movePlayerX;
    private float currentSpeed;
    private float lastMoveDirectionChangeTime = 0f; // Tiempo del último cambio de dirección

    void Start()
    {
        rbplayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        srPlayer = GetComponent<SpriteRenderer>();
        currentSpeed = speedPlayer;
    }

    void Update()
    {
        movePlayerX = Input.GetAxis("Horizontal");

        if (movePlayerX < 0)
        {
            srPlayer.flipX = true;
        }
        else if (movePlayerX > 0)
        {
            srPlayer.flipX = false;
        }

        animatorPlayer.SetBool("isMoving", Mathf.Abs(movePlayerX) > 0);
        animatorPlayer.SetBool("isRunning", Mathf.Abs(movePlayerX) > 0 && Input.GetKey(KeyCode.LeftShift));

        // Ajustar la velocidad según el estado de agachado y corriendo
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSpeed = crouchingSpeed;
            animatorPlayer.SetBool("isCrounching", true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            currentSpeed = walkingSpeed;
            animatorPlayer.SetBool("isCrounching", false);
            animatorPlayer.SetBool("isDragging", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Acelerar gradualmente al correr hasta alcanzar la velocidad máxima
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxRunningSpeed);
        }
        else
        {
            // Resetear la velocidad al dejar de correr
            currentSpeed = walkingSpeed;
        }
    }

    void FixedUpdate()
    {
        rbplayer.velocity = new Vector2(movePlayerX * currentSpeed, rbplayer.velocity.y);

        if (Mathf.Abs(movePlayerX) > 0)
        {
            lastMoveDirectionChangeTime = Time.time;
        }
    }
}
