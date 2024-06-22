using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Objeto del enemigo que se generará
    public float enemyInterval = 3.5f; // Intervalo de tiempo entre generaciones de enemigos
    public int minEnemies = 1; // Número mínimo de enemigos a generar
    public int maxEnemies = 3; // Número máximo de enemigos a generar
    public int maxTotalEnemies = 10; // Número máximo total de enemigos a crear antes de detener la generación

    // Color del gizmo
    public Color gizmoColor = Color.white;

    // Tamaño del gizmo
    public float gizmoSize = 1f;

    private int totalEnemiesSpawned = 0; // Contador de enemigos generados

    // Método para dibujar el gizmo en el editor
    void OnDrawGizmos()
    {
        // Establece el color del gizmo
        Gizmos.color = gizmoColor;

        // Dibuja una esfera para representar el rango de spawn
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }

    // Método que se llama al inicio
    void Start()
    {
        // Inicia la corutina para generar enemigos
        StartCoroutine(SpawnEnemies());
    }

    // Corutina para generar enemigos
    IEnumerator SpawnEnemies()
    {
        while (totalEnemiesSpawned < maxTotalEnemies)
        {
            // Espera el intervalo de tiempo especificado
            yield return new WaitForSeconds(enemyInterval);

            // Genera un número aleatorio de enemigos dentro del rango mínimo y máximo
            int numEnemies = Random.Range(minEnemies, Mathf.Min(maxEnemies, maxTotalEnemies - totalEnemiesSpawned) + 1);

            // Genera los enemigos
            for (int i = 0; i < numEnemies; i++)
            {
                // Posición aleatoria dentro de un rango determinado
                Vector3 spawnPosition = new Vector3(Random.Range(-2f, 2f), Random.Range(-3f, 3f), 0f);

                // Instancia el enemigo en la posición generada
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                // Incrementa el contador de enemigos generados
                totalEnemiesSpawned++;
            }
        }
    }
}
