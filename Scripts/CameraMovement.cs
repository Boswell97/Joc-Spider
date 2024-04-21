using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Objeto que seguirá la cámara, típicamente será el jugador
    public GameObject followObject;

    // Offset para ajustar la posición inicial de la cámara con respecto al jugador
    public Vector2 followOffset;

    // Si la cámara sigue al jugador en el eje X e Y
    public bool followXAxis = true;
    public bool followYAxis = true;

    // Velocidad de movimiento de la cámara
    public float speed = 3f;

    // Umbral para determinar cuándo la cámara debe comenzar a seguir al jugador
    private Vector2 threshold;

    // Rigidbody del jugador para obtener su velocidad
    private Rigidbody2D playerRigidbody;

    // Variable para verificar si el jugador está en el teleportador
    //private bool inTeleporter = false;

    // Método que se llama una vez al inicio
    void Start()
    {
        // Calcula el umbral inicial
        threshold = CalculateThreshold();

        // Obtiene el componente Rigidbody2D del jugador
        playerRigidbody = followObject.GetComponent<Rigidbody2D>();
    }

    // Método que se llama una vez por frame (físico)
    void FixedUpdate()
    {
        // Posición actual del jugador
        Vector2 playerPosition = followObject.transform.position;

        // Diferencias entre las posiciones de la cámara y del jugador
        float xDifference = Mathf.Abs(transform.position.x - playerPosition.x);
        float yDifference = Mathf.Abs(transform.position.y - playerPosition.y);

        // Nueva posición de la cámara
        Vector3 newPosition = transform.position;

        // Si followXAxis es verdadero y la diferencia en el eje X es mayor o igual al umbral en X
        if (followXAxis && xDifference >= threshold.x)
        {
            // La cámara se mueve hacia la posición X del jugador
            newPosition.x = playerPosition.x;
        }

        // Si followYAxis es verdadero y la diferencia en el eje Y es mayor o igual al umbral en Y
        if (followYAxis && yDifference >= threshold.y)
        {
            // La cámara se mueve hacia la posición Y del jugador
            newPosition.y = playerPosition.y;
        }

        // Velocidad de movimiento de la cámara
        float moveSpeed = playerRigidbody.velocity.magnitude > speed ? playerRigidbody.velocity.magnitude : speed;
        float auxi = moveSpeed;
        // Si el jugador está en el teleportador, aumenta la velocidad de la cámara
        //if ()
        //{
          //  moveSpeed *= 5; // Aumento de velocidad x5
            
       // }
        //else { moveSpeed = auxi; }

        // La cámara se mueve hacia la nueva posición
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }

    // Método para calcular el umbral basado en el tamaño de la pantalla y el offset
    private Vector2 CalculateThreshold()
    {
        // Tamaño de la pantalla en relación al tamaño ortográfico de la cámara
        Rect aspect = Camera.main.pixelRect;

        // Calcula el umbral en X e Y y le resta el offset
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;

        return t;
    }

    // Método para dibujar un gizmo que muestra el umbral en el editor de Unity
    private void OnDrawGizmos()
    {
        // Color del gizmo
        Gizmos.color = Color.blue;

        // Calcula el umbral
        Vector2 border = CalculateThreshold();

        // Dibuja un rectángulo alrededor de la cámara para representar el umbral
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }

    // Método para activar el flag de teleportador
    //public void EnterTeleporter()
    //{
      //  inTeleporter = true;
    //}

    // Método para desactivar el flag de teleportador
    //public void ExitTeleporter()
    //{
      //  inTeleporter = false;
    //}
}
