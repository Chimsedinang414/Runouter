using UnityEngine;

public class GiftSystem : MonoBehaviour
{
    [Header("Gift Spawning")]
    [SerializeField] private GameObject[] giftPrefabs; 
    [SerializeField] private Transform spawnPoint;     // Điểm sinh quà (phía bên phải)
    [SerializeField] private float spawnInterval = 1f; // Khoảng thời gian giữa mỗi lần spawn

    [Header("Gift Movement")]
    [SerializeField] private float moveSpeed = 5f;     // Tốc độ di chuyển sang trái
    [SerializeField] private float leftLimit = -10f;   // Giới hạn bên trái, nếu vượt quá thì xóa

    private float timer = 0f;

    void Update()
    {
        // Xử lý thời gian để sinh quà mới
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnGift();
            timer = 0f;
        }
    }

    /// <summary>
    /// Sinh ra một món quà ngẫu nhiên từ danh sách prefabs
    /// </summary>
    private void SpawnGift()
    {
        int index = Random.Range(0, giftPrefabs.Length);
        GameObject gift = Instantiate(giftPrefabs[index], spawnPoint.position, Quaternion.identity);

        // Thêm component để quản lý chuyển động của quà
        GiftMover giftMover = gift.AddComponent<GiftMover>();
        giftMover.Initialize(moveSpeed, leftLimit);
    }
}

 /// <summary>
/// Component quản lý chuyển động của từng món quà
/// </summary>
public class GiftMover : MonoBehaviour
{
    private float moveSpeed;
    private float leftLimit;

    public void Initialize(float speed, float limit)
    {
        moveSpeed = speed;
        leftLimit = limit;
    }

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