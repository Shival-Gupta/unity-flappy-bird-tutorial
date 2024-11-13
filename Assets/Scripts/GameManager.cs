using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;

    public int score { get; private set; } = 0;

    [Header("Next Level Settings")]
    [SerializeField] private int scoreThreshold = 6;
    [SerializeField] public string nextLevelName;
    [SerializeField] private string loadingSceneName = "LoadingScene";

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        for (int i = 0; i < obstacles.Length; i++)
        {
            Destroy(obstacles[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        // Check if the score has reached the threshold
        if (score >= scoreThreshold)
        {
            LoadLoadingScene();
        }
    }

    // Loads the loading scene first
    private void LoadLoadingScene()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            // Store next level name in PlayerPrefs
            PlayerPrefs.SetString("NextLevel", nextLevelName);
            PlayerPrefs.Save();  // Ensure it’s saved for loading

            Debug.Log($"Loading scene: {loadingSceneName} -> Next Level: {nextLevelName}");
            SceneManager.LoadScene(loadingSceneName);
        }
        else
        {
            Debug.LogWarning("Next Level name is not set in GameManager!");
        }
    }
}
