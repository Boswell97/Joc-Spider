using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAir : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    private Animator animatorPlayer;
    public float jumpPlayerForce = 280.0f;
    public float floatForce = 50.0f;
    public float maxFloatVelocity = 2.0f;
    private bool isFloating;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
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
    }

    void FixedUpdate()
    {
        // Manejar la flotación del jugador si está flotando
        if (isFloating)
        {
            Float();
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
            animatorPlayer.SetBool("isJumping", false);
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

        // Actualizar la animación de flotación
        animatorPlayer.SetBool("isFloating", isFloating);
    }
}
