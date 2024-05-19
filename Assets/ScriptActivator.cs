using UnityEngine;
using System.Collections.Generic;

public class ScriptActivator : MonoBehaviour
{
    public List<MonoBehaviour> scriptsToActivate = new List<MonoBehaviour>(); // Lista de scripts a activar
    public List<MonoBehaviour> scriptsToDeactivate = new List<MonoBehaviour>(); // Lista de scripts a desactivar
    public string boolParameterName; // Nombre del parámetro bool que activará o desactivará los scripts

    private Animator animator;
    private bool lastParameterValue; // Almacena el valor anterior del parámetro bool

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on this GameObject.");
        }

        // Inicializa el valor anterior del parámetro bool
        lastParameterValue = animator.GetBool(boolParameterName);
    }

    private void Update()
    {
        if (animator != null)
        {
            // Obtener el valor actual del parámetro bool del Animator
            bool currentParameterValue = animator.GetBool(boolParameterName);

            // Si el valor actual es diferente al valor anterior, actualiza el estado de los scripts
            if (currentParameterValue != lastParameterValue)
            {
                // Activa o desactiva los scripts según el valor del parámetro bool
                foreach (var script in scriptsToActivate)
                {
                    if (script != null)
                    {
                        script.enabled = currentParameterValue;
                    }
                }

                foreach (var script in scriptsToDeactivate)
                {
                    if (script != null)
                    {
                        script.enabled = !currentParameterValue;
                    }
                }

                // Actualiza el valor anterior del parámetro bool
                
            }
        }
    }
}

