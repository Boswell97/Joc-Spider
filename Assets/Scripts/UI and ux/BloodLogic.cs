using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodLogic : MonoBehaviour
{
    public ParticleSystem hitParticles;  // Sistema de part�culas para los efectos de sangre

    private bool hasTakenDamage = false;

    void Start()
    {
        if (hitParticles != null)
        {
            hitParticles.gameObject.SetActive(false);  // Desactivar el sistema de part�culas al inicio
        }
    }

    public void PlayBloodEffect()
    {
        if (!hasTakenDamage)
        {
            if (hitParticles != null)
            {
                hitParticles.gameObject.SetActive(true);  
                hitParticles.Play();
                hasTakenDamage = true;
            }
        }
        else
        {
            if (hitParticles != null)
            {
                hitParticles.Play();  
            }
        }
    }
}
