using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce= 15f;
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundCheckRadius = 0.2f;
    [SerializeField]
    private LayerMask groundLayer;
    private Animator animer;
    [SerializeField]
    private BoxCollider2D normalColider;
    [SerializeField]
    private CapsuleCollider2D underCollider;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player object.");
        }
        animer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckIfGrounded();
        HandleJump();
        HandleUnder();
        HandleSourceEffect();

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
        if(isGrounded && !AudioManager.instance.HasPlaySource())
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
            Destroy(collision.gameObject);
            //AudioManager.instance.PlayGiftSound();
        }
    }

}
