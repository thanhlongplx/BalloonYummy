using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int Level = 1;
    public GameObject PausePannel;
    public GameObject ChangeMusicPannel;
    public GameObject PauseButton;
    public int targetScore = 10; // point request to play sound
    public AudioClip scoreReachedClip; // This sound will be played when the target score is reached
    public AudioClip click;
    public int currentScore = 0; // Current score of the player
    private AudioSource audioSource; // AudioSource to play sounds
    public TextMeshProUGUI scoreText; // Tham chiếu đến TextMeshPro
    public TextMeshProUGUI levelText; // Tham chiếu đến TextMeshPro
    //panel highScore
    private int highScore; // variable saved high score
    private string highScoreDate; // variable saved high score date

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Thêm AudioSource
        LoadHighScore(); // Tải điểm cao nhất
        UpdateScoreText(); // Cập nhật hiển thị điểm ban đầu
        UpdateLevelText(); // Cập nhật hiển thị level ban đầu
    }

    void Update()
    {
        // Kiểm tra số điểm
        if (currentScore >= targetScore)
        {
            PlayScoreReachedSound();
            targetScore *= 2; // increse  target score by 2
            Level += 1;
            UpdateLevelText();
        }

        // Check input to leave, restart or pause game in keyboard
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void AddScore(int points)
    {
        currentScore += points; // increse current score by points
        UpdateScoreText(); // Cập nhật hiển thị điểm sau khi cộng

        // Kiểm tra và lưu điểm cao nhất
        if (currentScore > highScore)
        {
            highScore = currentScore;
            highScoreDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Lưu ngày giờ hiện tại
            SaveHighScore(); // Lưu điểm cao nhất và ngày giờ
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString(); // Cập nhật nội dung TextMeshPro
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level " + Level.ToString(); // Cập nhật nội dung TextMeshPro
    }

    private void PlayScoreReachedSound()
    {
        if (scoreReachedClip != null)
        {
            audioSource.PlayOneShot(scoreReachedClip); // Phát âm thanh
        }
    }

    public void ExitGame()
    {
        Application.Quit(); // Leave game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // pause game in Editor
#endif
    }

    public void RestartGame()
    {
        if (click != null)
        {
            audioSource.PlayOneShot(click);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene

    }

    public void PauseGame()
    {
        //Pause or continue the game
        if (Time.timeScale == 1)
        {
            if (click != null)
            {
                audioSource.PlayOneShot(click);
            }
            Time.timeScale = 0; // Pause game time
            ShowHidePanel(true); // Show panel
            PauseButton.SetActive(false);
        }
        else
        {
            if (click != null)
            {
                audioSource.PlayOneShot(click);
            }
            Time.timeScale = 1; //Continue game
            ShowHidePanel(false); // Hide panel
            PauseButton.SetActive(true);
        }
    }
    public void changeMusic()
    {
        showChangeMusicPanel(ChangeMusicPannel);
    }
    private void showChangeMusicPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf); //Convert current status
    }

    private void ShowHidePanel(bool isVisible)
    {
        PausePannel.SetActive(isVisible); // Show or hide the panel
    }

    public void LoadSceneByName(string sceneName)
    {
        // Check the sound
        if (click != null)
        {
            audioSource.PlayOneShot(click);
        }
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene không tồn tại: " + sceneName);
        }
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Saved high score, default is 0
        highScoreDate = PlayerPrefs.GetString("HighScoreDate", "Get High Score To Show"); // Saved high score date, default is "Chưa có"

    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore); // Lưu điểm cao nhất
        PlayerPrefs.SetString("HighScoreDate", highScoreDate); // Lưu ngày đạt điểm cao nhất
        PlayerPrefs.Save(); // Lưu các thay đổi

    }
}