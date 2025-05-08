using UnityEngine;

public class SpamGift : MonoBehaviour
{
    [SerializeField] private GameObject[] giftPrefabs; // Cho phép random nhiều loại quà
    [SerializeField] private Transform spawnPoint;      // Điểm sinh quà (phía bên phải)
    [SerializeField] private float moveSpeed = 5f;      // Tốc độ di chuyển của quà
    [SerializeField] private float spawnInterval = 1f;  // Khoảng thời gian giữa mỗi lần spawn

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnGift();
            timer = 0f;
        }
    }

    private void SpawnGift()
    {
        int index = Random.Range(0, giftPrefabs.Length);
        GameObject gift = Instantiate(giftPrefabs[index], spawnPoint.position, Quaternion.identity);

        GiftMove giftMove = gift.GetComponent<GiftMove>();
        if (giftMove != null)
        {
            giftMove.moveSpeed = moveSpeed;
        }
    }
}
