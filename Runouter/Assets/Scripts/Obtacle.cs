using UnityEngine;

public class Obtacle : MonoBehaviour
{

    [SerializeField] private float leftboundary = -10f;
    [SerializeField] private int health = 1;
    //  [SerializeField] private GameObject destroyEffect;
    //


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        moveObtacle();
    }
    private void moveObtacle()
    {
        transform.position += Vector3.left * GameManager.instance.GetGameSpeed() * Time.deltaTime;
        if (transform.position.x < leftboundary)
        {
            Destroy(gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
        }


    }

    public virtual void TakeDamage(int damage = 1)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
       /*if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
       */


        Destroy(gameObject);

    }
}
