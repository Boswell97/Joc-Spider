using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public Transform targetTransform; // Transform hacia el que se moverá
    public float speed = 5f; // Velocidad de movimiento

    private HealthConceptEnemy health;

    void Start()
    {
        health = GetComponent<HealthConceptEnemy>();

        if (health == null)
        {
            Debug.LogWarning("HealthConceptEnemy component not found on this GameObject.");
        }
    }

    void Update()
    {
        if (health != null && health.isAlive)
        {
            if (targetTransform != null)
            {
                
                Vector3 direction = targetTransform.position - transform.position;

              
                Vector3 movement = direction.normalized * speed * Time.deltaTime;

               
                transform.Translate(movement);
            }
           
        }
    }
}
