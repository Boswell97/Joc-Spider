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

        // Actualizar la animación de salto y flotación
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
        // Manejar la flotación del jugador si está flotando
        if (isFloating)
        {
            Float();
        }
        
        // Restablecer el estado de flotación cuando el jugador toca el suelo
        if (tocandoSuelos.IsGrounded)
        {
            isFloating = false;
        }
    }

    private void Jump()
    {
        // Saltar solo si el jugador no está flotando actualmente
        if (!isFloating)
        {
            // Aplicar la fuerza de salto hacia arriba
            rbPlayer.AddForce(Vector2.up * jumpPlayerForce);

            // Activar el flag de flotación
            isFloating = true;

            // Desactivar la animación de salto para evitar conflictos con la flotación
          //  animatorPlayer.SetBool("isJumping", false);
        }
        
    }

    private void Float()
    {
        // Aplicar una fuerza hacia arriba para la flotación
        rbPlayer.AddForce(Vector2.up * floatForce);

        // Limitar la velocidad máxima de flotación
        if (rbPlayer.velocity.y > maxFloatVelocity)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, maxFloatVelocity);
        }
    }

    private void UpdateAnimations()
    {
        // Actualizar la animación de salto
        animatorPlayer.SetBool("isJumping", rbPlayer.velocity.y > 0);

        // Verificar si el jugador está en el suelo y ajustar la animación de flotación
        animatorPlayer.SetBool("isFloating", rbPlayer.velocity.y < 0);

       
    }
}
