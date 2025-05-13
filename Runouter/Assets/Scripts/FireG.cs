using UnityEngine;

public class FireG : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 25f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private bool useGravity = false; 

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
    }

    private void Start()
    {
        // Tự hủy 
        Destroy(gameObject, lifeTime);

        // Thiết lập vận tốc ban đầu
        rb.linearVelocity = transform.right * initialSpeed;

        
        rb.gravityScale = useGravity ? 1f : 0f;

      
        rb.freezeRotation = true;
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Obtacle"))
        {
            Obtacle obtacle = collision.GetComponent<Obtacle>();
            if (obtacle != null)
            {
                obtacle.TakeDamage();
            }
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Player") && !collision.CompareTag("Gift"))
        {
            Destroy(gameObject);
        }
    }
}