using UnityEngine;

public class RandomOb : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private Transform highPos;
    [SerializeField] private Transform lowPos;
    private float timer = 0;
    [SerializeField] private float spawRate = 2f;
  

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawRate)
        {
            spawnObtacle();
            timer = 0;
        }
    }
    private void spawnObtacle()
    {
        int randomIndex = Random.Range(0, objects.Length);
        GameObject obtacle = null;
        if (randomIndex == 0 || randomIndex == 1)
        {
            obtacle = Instantiate(objects[randomIndex], lowPos.position, Quaternion.identity);
        }
        else if (randomIndex == 2)
        {
           obtacle = Instantiate(objects[randomIndex], highPos.position, Quaternion.identity);
        }
        // Đảm bảo đối tượng có component Obtacle
        if (obtacle.GetComponent<Obtacle>() == null)
        {
            // Nếu chưa có component Obtacle, thêm vào
            Obtacle obtacleComponent = obtacle.AddComponent<Obtacle>();

            // Đặt tag để đảm bảo logic va chạm hoạt động
            obtacle.tag = "Obtacle";
        }
    }

}