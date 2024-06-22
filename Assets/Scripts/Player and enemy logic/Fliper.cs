using UnityEngine;

public class Fliper : MonoBehaviour
{
    public Transform targetTransform; // Transform hacia el que se moverá

    private Transform elEnemy;

    private void Start()
    {
        elEnemy = transform;
    }

    private void Update()
    {

        if (targetTransform != null)
        {
            // Calcula la dirección hacia el objetivo
            Vector3 direction = targetTransform.position - transform.position;

            // Gira hacia el objetivo
            FlipTowards(direction);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un Transform objetivo.");
        }
    }

    private void FlipTowards(Vector3 direction)
    {
        // Si la dirección es negativa en x y la escala es positiva o la dirección es positiva en x y la escala es negativa
        if ((direction.x < 0 && elEnemy.localScale.x > 0) || (direction.x > 0 && elEnemy.localScale.x < 0))
        {
            Vector3 escala = elEnemy.localScale;
            escala.x *= -1;
            elEnemy.localScale = escala;
        }
    }
}


