using UnityEngine;
using UnityEngine.UI; // Thêm để hỗ trợ hiển thị UI nếu bạn muốn hiển thị số đạn

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 15f;
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private Animator animer;
    [SerializeField] private BoxCollider2D normalColider;
    [SerializeField] private CapsuleCollider2D underCollider;




    // Fire system
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float fireRate = 0.15f;
    private float nextFireTime = 0f;
    [SerializeField] private int maxFireCount = 10; // Giới hạn số đạn tối đa
    public int fireCount = 0; // Số đạn hiện tại

    // UI cho số đạn (tùy chọn)
    [SerializeField] private Text fireCountText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player object.");
        }

        animer = GetComponent<Animator>();

        if (firePoint == null)
        {
            Debug.LogError("Fire point transform not assigned.");
        }

        
        //UpdateFireCountUI();
    }

    void Update()
    {
        isGrounded = CheckIfGrounded();
        HandleJump();
        HandleUnder();
        HandleSourceEffect();
        HandleShooting();
    }

    private bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }
    }

    private void HandleUnder()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            normalColider.enabled = false;
            underCollider.enabled = true;
            animer.SetBool("Change", true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            normalColider.enabled = true;
            underCollider.enabled = false;
            animer.SetBool("Change", false);
        }
    }

    private void HandleSourceEffect()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            AudioManager.instance.PlayJumpSound();
        }

        if (isGrounded && !AudioManager.instance.HasPlaySource())
        {
            AudioManager.instance.PlayTapSound();
            AudioManager.instance.SetPlaySource(true);
        }
        else if (!isGrounded && AudioManager.instance.HasPlaySource())
        {
            AudioManager.instance.SetPlaySource(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obtacle"))
        {
            AudioManager.instance.PlayHurtSound();
        }
        else if (collision.CompareTag("Gift"))
        {
            // Tăng số đạn khi nhận gift
            AddFireCount(1);

           
           AudioManager.instance.PlayGiftSound();

            
            Destroy(collision.gameObject);
        }
    }

    
    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.F) && fireCount > 0 && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    
    void Shoot()
    {
        if (firePrefab != null)
        {
            Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("bulletPrefab is null or destroyed!");
        }

       
        nextFireTime = Time.time + fireRate;

        // Giảm số đạn
        fireCount--;

        // 
       // UpdateFireCountUI();


       if (AudioManager.instance != null)
        {
             AudioManager.instance.PlayFireSound(); 
        }
    }

  
    public void AddFireCount(int amount)
    {
        fireCount = Mathf.Min(fireCount + amount, maxFireCount);
       // UpdateFireCountUI();
    }

   /* private void UpdateFireCountUI()
    {
        if (fireCountText != null)
        {
            fireCountText.text = "Đạn: " + fireCount;
        }
    }
   */
}