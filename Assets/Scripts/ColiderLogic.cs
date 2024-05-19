using System.Collections.Generic;
using UnityEngine;

public class ColliderLogic : MonoBehaviour
{
    // Este script solo sirve para hacer desaparecer el collider cuando se activa cierta animación.
    // No me ha dado tiempo de implementar la patada baja, que es para lo que servía este script en combinación con otro
    // que hiciera lo contrario a este: crear un collider para animaciones específicas.
    // Dejo comentado cómo sería para moverlo en vez de deshabilitarlo.
    

    [System.Serializable]
    public class AnimationColliderControl
    {
        public string animationParameter; // Parámetro de animación que controla el collider
        //public Vector2 offsetPosition;  // Nueva posición para el collider (si se quiere mover en vez de desactivar)
    }

    public List<AnimationColliderControl> animationControls = new List<AnimationColliderControl>();

    private Animator animator;
    public Collider2D targetCollider;

    void Start()
    {
        // Si el Animator no está en el objeto, buscarlo en el padre
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInParent<Animator>();
        }

        if (targetCollider == null)
        {
            Debug.LogWarning("Target Collider2D is not assigned.");
        }
    }

    void Update()
    {
        if (animator != null && targetCollider != null)
        {
            bool shouldDisableCollider = false;

            foreach (var animControl in animationControls)
            {
                if (animator.GetBool(animControl.animationParameter))
                {
                    shouldDisableCollider = true;

                    //  mover  collider en vez de desactivarlo:


                    // Vector3 newPosition = targetCollider.transform.position;
                    // newPosition.x += animControl.offsetPosition.x;
                    // newPosition.y += animControl.offsetPosition.y;
                    // targetCollider.transform.position = newPosition;

                }
            }

            targetCollider.enabled = !shouldDisableCollider;
        }
    }
}
