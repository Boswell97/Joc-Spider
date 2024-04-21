using UnityEngine;

public class AirMovement : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    private Animator animatorPlayer;
    private TocandoSuelos tocandoSuelos; // Referencia al script TocandoSuelos
    public float jumpPlayerForce = 280.0f;
    public float floatForce = 50.0f;
    public float maxFloatVelocity = 2.0f;
    private bool isFloating;
    bool Ground;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        //Ground = GetComponen<TocandoSuelos>();

        // Obtener la referencia al componente TocandoSuelos
        tocandoSuelos = GetComponent<TocandoSuelos>();
    }

    void Update()
    {
        // Saltar cuando se presiona la tecla de flecha hacia arriba (UpArrow)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        // Actualizar la animaci�n de salto y flotaci�n
        UpdateAnimations();
          if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isFloating = false;
            animatorPlayer.SetBool("isJumping", false);
            animatorPlayer.SetBool("isFloating", false);
        }
    }

    void FixedUpdate()
    {
        // Manejar la flotaci�n del jugador si est� flotando
        if (isFloating)
        {
            Float();
        }
        
        // Restablecer el estado de flotaci�n cuando el jugador toca el suelo
        if (tocandoSuelos.IsGrounded)
        {
            isFloating = false;
        }
    }

    private void Jump()
    {
        // Saltar solo si el jugador no est� flotando actualmente
        if (!isFloating)
        {
            // Aplicar la fuerza de salto hacia arriba
            rbPlayer.AddForce(Vector2.up * jumpPlayerForce);

            // Activar el flag de flotaci�n
            isFloating = true;

            // Desactivar la animaci�n de salto para evitar conflictos con la flotaci�n
          //  animatorPlayer.SetBool("isJumping", false);
        }
        
    }

    private void Float()
    {
        // Aplicar una fuerza hacia arriba para la flotaci�n
        rbPlayer.AddForce(Vector2.up * floatForce);

        // Limitar la velocidad m�xima de flotaci�n
        if (rbPlayer.velocity.y > maxFloatVelocity)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, maxFloatVelocity);
        }
    }

    private void UpdateAnimations()
    {
        // Actualizar la animaci�n de salto
        animatorPlayer.SetBool("isJumping", rbPlayer.velocity.y > 0);

        // Verificar si el jugador est� en el suelo y ajustar la animaci�n de flotaci�n
        animatorPlayer.SetBool("isFloating", rbPlayer.velocity.y < 0);

       
    }
}
