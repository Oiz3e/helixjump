using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelWin;

    public GameObject gameOverPanel;
    public GameObject levelWinPanel;

    public static int CurrentLevelIndex;
    public static int noOfPassingRings;

    public TextMeshProUGUI currentLevelIndex;
    public TextMeshProUGUI nextLevelIndex;
    public Button resetButton;
    public Button playAgainButton;
    public Slider ProgressBar;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI gameOverScoreText;

    private int initialScore;

    public void Awake()
    {
        CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        ScoreManager.score = PlayerPrefs.GetInt("PlayerScore", 0);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        noOfPassingRings = 0;
        gameOver = false;
        levelWin = false;
        resetButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        
        initialScore = ScoreManager.score;
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            resetButton.gameObject.SetActive(true);
            playAgainButton.gameObject.SetActive(true);
            gameOverScoreText.text = "Total Score: " + ScoreManager.score;
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            resetButton.gameObject.SetActive(false);
            playAgainButton.gameObject.SetActive(false);
        }

        currentLevelIndex.text = CurrentLevelIndex.ToString();
        nextLevelIndex.text = (CurrentLevelIndex + 1).ToString();

        int progress = noOfPassingRings * 100 / FindObjectOfType<HelixManager>().noOfRings;
        ProgressBar.value = progress;

        scoreText.text = " " + ScoreManager.score;

        if (levelWin)
        {
            levelWinPanel.SetActive(true);
            totalScoreText.text = "Total Score: " + ScoreManager.score;
            scoreText.gameObject.SetActive(false);

            if (Input.GetMouseButtonDown(0))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", CurrentLevelIndex + 1);
                PlayerPrefs.SetInt("PlayerScore", ScoreManager.score);
                PlayerPrefs.Save();
                SceneManager.LoadScene(0);
            }
        }
    }

    public void ResetLevels()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", 1);
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void PlayAgain()
    {
        ScoreManager.score = initialScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueLevel()
    {
        Time.timeScale = 1f;
        gameOver = false;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnResetTextClicked()
    {
        ResetLevels();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("PlayerScore", ScoreManager.score);
        PlayerPrefs.Save();
    }
}