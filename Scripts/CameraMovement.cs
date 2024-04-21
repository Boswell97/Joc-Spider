using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Objeto que seguir� la c�mara, t�picamente ser� el jugador
    public GameObject followObject;

    // Offset para ajustar la posici�n inicial de la c�mara con respecto al jugador
    public Vector2 followOffset;

    // Si la c�mara sigue al jugador en el eje X e Y
    public bool followXAxis = true;
    public bool followYAxis = true;

    // Velocidad de movimiento de la c�mara
    public float speed = 3f;

    // Umbral para determinar cu�ndo la c�mara debe comenzar a seguir al jugador
    private Vector2 threshold;

    // Rigidbody del jugador para obtener su velocidad
    private Rigidbody2D playerRigidbody;

    // Variable para verificar si el jugador est� en el teleportador
    //private bool inTeleporter = false;

    // M�todo que se llama una vez al inicio
    void Start()
    {
        // Calcula el umbral inicial
        threshold = CalculateThreshold();

        // Obtiene el componente Rigidbody2D del jugador
        playerRigidbody = followObject.GetComponent<Rigidbody2D>();
    }

    // M�todo que se llama una vez por frame (f�sico)
    void FixedUpdate()
    {
        // Posici�n actual del jugador
        Vector2 playerPosition = followObject.transform.position;

        // Diferencias entre las posiciones de la c�mara y del jugador
        float xDifference = Mathf.Abs(transform.position.x - playerPosition.x);
        float yDifference = Mathf.Abs(transform.position.y - playerPosition.y);

        // Nueva posici�n de la c�mara
        Vector3 newPosition = transform.position;

        // Si followXAxis es verdadero y la diferencia en el eje X es mayor o igual al umbral en X
        if (followXAxis && xDifference >= threshold.x)
        {
            // La c�mara se mueve hacia la posici�n X del jugador
            newPosition.x = playerPosition.x;
        }

        // Si followYAxis es verdadero y la diferencia en el eje Y es mayor o igual al umbral en Y
        if (followYAxis && yDifference >= threshold.y)
        {
            // La c�mara se mueve hacia la posici�n Y del jugador
            newPosition.y = playerPosition.y;
        }

        // Velocidad de movimiento de la c�mara
        float moveSpeed = playerRigidbody.velocity.magnitude > speed ? playerRigidbody.velocity.magnitude : speed;
        float auxi = moveSpeed;
        // Si el jugador est� en el teleportador, aumenta la velocidad de la c�mara
        //if ()
        //{
          //  moveSpeed *= 5; // Aumento de velocidad x5
            
       // }
        //else { moveSpeed = auxi; }

        // La c�mara se mueve hacia la nueva posici�n
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }

    // M�todo para calcular el umbral basado en el tama�o de la pantalla y el offset
    private Vector2 CalculateThreshold()
    {
        // Tama�o de la pantalla en relaci�n al tama�o ortogr�fico de la c�mara
        Rect aspect = Camera.main.pixelRect;

        // Calcula el umbral en X e Y y le resta el offset
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;

        return t;
    }

    // M�todo para dibujar un gizmo que muestra el umbral en el editor de Unity
    private void OnDrawGizmos()
    {
        // Color del gizmo
        Gizmos.color = Color.blue;

        // Calcula el umbral
        Vector2 border = CalculateThreshold();

        // Dibuja un rect�ngulo alrededor de la c�mara para representar el umbral
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }

    // M�todo para activar el flag de teleportador
    //public void EnterTeleporter()
    //{
      //  inTeleporter = true;
    //}

    // M�todo para desactivar el flag de teleportador
    //public void ExitTeleporter()
    //{
      //  inTeleporter = false;
    //}
}
