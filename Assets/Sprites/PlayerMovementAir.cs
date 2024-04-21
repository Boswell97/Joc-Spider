using UnityEngine;

public class PlayerMovementAir : MonoBehaviour
{
    private Rigidbody2D rbplayer;
    public float jumpPlayerForce = 280.0f;
    public bool isFloating; // Nuevo flag para controlar si el jugador está flotando
    public float floatForce = 50.0f; // Fuerza de flotación cuando se mantiene presionada la tecla de salto
    public float maxFloatVelocity = 2.0f; // Velocidad máxima de flotación
    public Animator animatorPlayer;

    void Start()
    {
        rbplayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
    }

    void Update()
    {
        // Saltar cuando se presiona la tecla de flecha hacia arriba (UpArrow)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        // Actualizar la animación isFloating si el jugador está flotando
        animatorPlayer.SetBool("isFloating", isFloating);
        // Actualizar la animación isAir si el jugador no está en el suelo
        // animatorPlayer.SetBool("isAir", !IsGrounded());
    }

    void FixedUpdate()
    {
        // Manejar la flotación del jugador si está flotando
        if (isFloating)
        {
            // Aplicar una fuerza hacia arriba para la flotación
            rbplayer.AddForce(Vector2.up * floatForce);

            // Limitar la velocidad máxima de flotación
            if (rbplayer.velocity.y > maxFloatVelocity)
            {
                rbplayer.velocity = new Vector2(rbplayer.velocity.x, maxFloatVelocity);
            }
        }
    }

    private void Jump()
    {
        // Saltar solo si el jugador no está flotando actualmente
        if (!isFloating)
        {
            // Aplicar la fuerza de salto hacia arriba
            rbplayer.AddForce(Vector2.up * jumpPlayerForce);

            // Activar el flag de flotación
            isFloating = true;

            // Desactivar la animación de salto para evitar conflictos con la flotación
            animatorPlayer.SetBool("isJumping", false);
        }
    }

    private bool IsGrounded()
    {
        // Raycast hacia abajo para verificar si el jugador está en el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null && hit.collider.CompareTag("Ground");
    }
}