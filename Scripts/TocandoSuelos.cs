using UnityEngine;

public class TocandoSuelos : MonoBehaviour
{
    public ContactFilter2D filtro;
    public float DistanciaSuelo = 0.05f;
    private CapsuleCollider2D ColToca;
    public Animator animatorPlayer;
    private RaycastHit2D[] tocaSuelos = new RaycastHit2D[5];
    private bool isGrounded;

    public bool IsGrounded
    {
        get { return isGrounded; }
        private set
        {
            isGrounded = value;
            animatorPlayer.SetBool("isGrounded", value);
        }
    }

    private void Awake()
    {
        ColToca = GetComponent<CapsuleCollider2D>();
        animatorPlayer = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        IsGrounded = ColToca.Cast(Vector2.down, filtro, tocaSuelos, DistanciaSuelo) > 0;
    }
}
