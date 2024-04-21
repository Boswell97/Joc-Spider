using UnityEngine;

public class PlayerMovementAir : MonoBehaviour
{
    private Rigidbody2D rbplayer;
    public float jumpPlayerForce = 280.0f;
    public bool isFloating; // Nuevo flag para controlar si el jugador est� flotando
    public float floatForce = 50.0f; // Fuerza de flotaci�n cuando se mantiene presionada la tecla de salto
    public float maxFloatVelocity = 2.0f; // Velocidad m�xima de flotaci�n
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
        // Actualizar la animaci�n isFloating si el jugador est� flotando
        animatorPlayer.SetBool("isFloating", isFloating);
        // Actualizar la animaci�n isAir si el jugador no est� en el suelo
        // animatorPlayer.SetBool("isAir", !IsGrounded());
    }

    void FixedUpdate()
    {
        // Manejar la flotaci�n del jugador si est� flotando
        if (isFloating)
        {
            // Aplicar una fuerza hacia arriba para la flotaci�n
            rbplayer.AddForce(Vector2.up * floatForce);

            // Limitar la velocidad m�xima de flotaci�n
            if (rbplayer.velocity.y > maxFloatVelocity)
            {
                rbplayer.velocity = new Vector2(rbplayer.velocity.x, maxFloatVelocity);
            }
        }
    }

    private void Jump()
    {
        // Saltar solo si el jugador no est� flotando actualmente
        if (!isFloating)
        {
            // Aplicar la fuerza de salto hacia arriba
            rbplayer.AddForce(Vector2.up * jumpPlayerForce);

            // Activar el flag de flotaci�n
            isFloating = true;

            // Desactivar la animaci�n de salto para evitar conflictos con la flotaci�n
            animatorPlayer.SetBool("isJumping", false);
        }
    }

    private bool IsGrounded()
    {
        // Raycast hacia abajo para verificar si el jugador est� en el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null && hit.collider.CompareTag("Ground");
    }
}