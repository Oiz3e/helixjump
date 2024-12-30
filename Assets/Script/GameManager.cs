using System.Collections; 
using System.Collections.Generic;
using TMPro;
using UnityEngine; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour { 
    public static bool gameOver; 
    public static bool levelWin;

    public GameObject gameOverPanel;
    public GameObject levelWinPanel;

    public static int CurrentLevelIndex;
    public static int noOfPassingRings;

    public TextMeshProUGUI currentLevelIndex;
    public TextMeshProUGUI nextLevelIndex;

    public Slider ProgressBar;

    public void Awake() {
        CurrentLevelIndex = PlayerPrefs.GetInt ("CurrentLevelIndex", 1);
    }

    private void Start() {
        Time.timeScale = 1f;
        noOfPassingRings = 0;
        gameOver = false;
        levelWin = false;
    }

    private void Update () {
        if (gameOver) {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            if(Input.GetMouseButtonDown(0)) {
                SceneManager.LoadScene(0);
            }
        }

        currentLevelIndex.text = CurrentLevelIndex.ToString ();
        nextLevelIndex.text = (CurrentLevelIndex + 1).ToString ();

        int proggress = noOfPassingRings * 100 / FindObjectOfType<HelixManager> ().noOfRings;
        ProgressBar.value = proggress;

        if(levelWin) {
            levelWinPanel.SetActive(true);
            if(Input.GetMouseButtonDown(0)) {
                PlayerPrefs.SetInt ("CurrentLevelIndex", CurrentLevelIndex + 1);
                SceneManager.LoadScene(0);
            }
        }
    }
}