using UnityEngine;

public class GiftMove : MonoBehaviour
{
    public float moveSpeed = 5f;     // Tốc độ di chuyển sang trái
    public float leftLimit = -10f;   // Giới hạn bên trái, nếu vượt quá thì xóa

    void Update()
    {
        MoveGift();
    }

    private void MoveGift()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < leftLimit)
        {
            Destroy(gameObject);
        }
    }
}
