using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    public string deathTriggerParameter = "Die";
    public GameObject[] objectsToFreeze; 
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleDeath()
    {
        if (animator != null)
        {
            animator.SetTrigger(deathTriggerParameter);
             
        }


        
        foreach (var obj in objectsToFreeze)
        {
            if (obj != null)
            {
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                }
            }
        }
      
        //pa que espere antes de destruirse
        Invoke("DeathAfterAnime", 12f);
    }
    public void DeathAfterAnime()
    {
        
        Destroy(gameObject);
    }
}
