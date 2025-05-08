using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{ 
    public static GameManager instance;
    private float gameSpeed = 5f;
    [SerializeField]
    private float speedIncrease = 0.1f;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private float score = 0;
    [SerializeField] private GameObject scoreObject;
    [SerializeField] private GameObject gameStartMesh;
    [SerializeField] private GameObject gameOverMesh;
    private bool isGameOver = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else { 
            Destroy(gameObject);
        }
    }
    public float GetGameSpeed()
    {
        return gameSpeed;
    }
    private void UpdateGameSpeed()
    {
        gameSpeed = gameSpeed + speedIncrease * Time.deltaTime;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        HandleStartGame();
        if (!isGameOver)
        {
            UpdateGameSpeed();
            UpdateScore();
        }
      
    }
    private void UpdateScore()
    {
        score += Time.deltaTime * 10;
      
            scoreText.text = "Score:" + Mathf.FloorToInt(score);
        
    }
    private void StartGame()
    {
        Time.timeScale = 0;
        scoreObject.SetActive(false);
        gameStartMesh.SetActive(true);
        gameOverMesh.SetActive(false);

    }
    public  void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverMesh.SetActive(true);
        scoreObject.SetActive(false);
        StartCoroutine(ReloadScren());


    }
    private void HandleStartGame()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            scoreObject.SetActive(true);
            gameStartMesh.SetActive(false);

        
        }

    }
    private IEnumerator ReloadScren()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
