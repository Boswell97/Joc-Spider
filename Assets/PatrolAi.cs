using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAi : MonoBehaviour
{
    public GameObject puntoIzquierdo;
    public GameObject puntoDerecho;
    private Rigidbody2D rb;
    private Animator anim;
    public Transform puntoActual;
    public float velocidad;
    public string WalikingAnimationName;
    private bool mirandoDerecha = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        puntoActual = puntoDerecho.transform;
        anim.SetBool(WalikingAnimationName, true);
    }

    // Update is called once per frame
    void Update()
    {
        // Mover hacia el punto actual
        Vector2 direccion = (puntoActual.position - transform.position).normalized;
        rb.velocity = new Vector2(direccion.x * velocidad, rb.velocity.y);

        // Verificar la distancia al punto actual
        float distancia = Vector2.Distance(transform.position, puntoActual.position);

        // Cambiar de dirección si la distancia es menor a 1.5 el sprite que usamos para venom es gordo adaptar este tamaño en base al sprite
        if (distancia < 1.5f)
        {
            puntoActual = puntoActual == puntoIzquierdo.transform ? puntoDerecho.transform : puntoIzquierdo.transform;

            // Llamar a Flip cuando se cambie de punto
            Flip();
        }
    }

    public void Flip()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        //no es neces pero ayuda para ver 
        Gizmos.DrawWireSphere(puntoIzquierdo.transform.position, 0.5f);
        Gizmos.DrawWireSphere(puntoDerecho.transform.position, 0.5f);
        Gizmos.DrawLine(puntoDerecho.transform.position, puntoIzquierdo.transform.position);
    }

    public void StopPatrol()
    {
        //para empezar el chase a nivel de animator es igual pero chase se salta los puntos de patrol
        anim.SetBool(WalikingAnimationName, false);
        rb.velocity = Vector2.zero;
    }
}
