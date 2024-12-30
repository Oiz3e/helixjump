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
    public Button resetButton; // Referensi ke Button reset
    public Button playAgainButton; // Referensi ke Button play again

    public Slider ProgressBar;

    public void Awake()
    {
        CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        noOfPassingRings = 0;
        gameOver = false;
        levelWin = false;
        resetButton.gameObject.SetActive(false); // Sembunyikan tombol reset di awal
        playAgainButton.gameObject.SetActive(false); // Sembunyikan tombol play again di awal
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            resetButton.gameObject.SetActive(true); // Tampilkan tombol reset ke level 1 saat game over
            playAgainButton.gameObject.SetActive(true); // Tampilkan tombol play again saat game over

            // Cek input untuk melanjutkan level
            if (Input.GetMouseButtonDown(0)) // Jika layar ditekan
            {
                // Hanya melanjutkan level jika tombol play again tidak ditekan
                // Kita tidak memanggil ContinueLevel() di sini
            }
        }
        else
        {
            resetButton.gameObject.SetActive(false); // Sembunyikan tombol reset jika tidak game over
            playAgainButton.gameObject.SetActive(false); // Sembunyikan tombol play again jika tidak game over
        }

        currentLevelIndex.text = CurrentLevelIndex.ToString();
        nextLevelIndex.text = (CurrentLevelIndex + 1).ToString();

        int progress = noOfPassingRings * 100 / FindObjectOfType<HelixManager>().noOfRings;
        ProgressBar.value = progress;

        if (levelWin)
        {
            levelWinPanel.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", CurrentLevelIndex + 1);
                SceneManager.LoadScene(0);
            }
        }
    }

    public void ResetLevels()
    {
        Debug.Log("Current Level Index before reset: " + PlayerPrefs.GetInt("CurrentLevelIndex"));
        PlayerPrefs.SetInt("CurrentLevelIndex", 1); // Set level ke 1
        PlayerPrefs.Save(); // Simpan perubahan
        Debug.Log("Resetting level to level 1."); // Log untuk reset level
        SceneManager.LoadScene(0); // Memuat ulang scene (pastikan scene 0 adalah level awal)
    }

    public void PlayAgain()
    {
        Debug.Log("Playing again."); // Log saat mengulang level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Memuat ulang scene saat ini
    }

    public void ContinueLevel()
    {
        Debug.Log("Continuing level."); // Log saat mengulang level
        Time.timeScale = 1f; // Mengaktifkan kembali waktu
        gameOver = false; // Set gameOver ke false
        gameOverPanel.SetActive(false); // Sembunyikan panel game over

        // Memuat ulang level saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Memuat ulang scene saat ini
    }

    public void OnResetTextClicked() // Fungsi ini dipanggil saat tombol reset ditekan
    {
        Debug.Log("Reset text clicked."); // Log saat teks reset diklik
        ResetLevels(); // Panggil fungsi reset level
    }
}