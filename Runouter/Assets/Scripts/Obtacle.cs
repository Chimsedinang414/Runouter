using UnityEngine;

public class Obtacle : MonoBehaviour
{

    public float leftboundary = -10f;
   // public float gamespeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    // Update is called once per frame
    void Update()
    {
        moveObtacle();
    }
    private void moveObtacle()
    {
        transform.position += Vector3.left * GameManager.instance.GetGameSpeed() * Time.deltaTime;
        if(transform.position.x < leftboundary)
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
}
