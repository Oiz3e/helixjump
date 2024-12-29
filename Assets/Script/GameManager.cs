using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour { 
    public static bool gameOver; public static bool levelWin;

    public GameObject gameOverPanel;
    public GameObject levelWinPanel;

    private void Start() {
        Time.timeScale = 1f;
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

        if(levelWin) {
            levelWinPanel.SetActive(true);
            if(Input.GetMouseButtonDown(0)) {
                SceneManager.LoadScene(0);
            }
        }
    }
}