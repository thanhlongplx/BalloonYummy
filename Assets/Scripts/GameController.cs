using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int Level = 1;
    public GameObject PausePannel;
    public GameObject ChangeMusicPannel;
    public GameObject PauseButton;
    public int targetScore = 10; // Mức điểm để phát âm thanh
    public AudioClip scoreReachedClip; // Âm thanh phát khi đạt được điểm
    public AudioClip click;
    public int currentScore = 0; // Điểm hiện tại
    private AudioSource audioSource; // AudioSource để phát âm thanh
    public TextMeshProUGUI scoreText; // Tham chiếu đến TextMeshPro
    public TextMeshProUGUI levelText; // Tham chiếu đến TextMeshPro
    private int highScore; // Biến lưu điểm cao nhất
    private string highScoreDate; // Biến lưu ngày đạt điểm cao nhất

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
            targetScore *= 2; // Tăng mức điểm mục tiêu
            Level += 1;
            UpdateLevelText();
        }

        // Kiểm tra input để thoát, restart hoặc pause game
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
        currentScore += points; // Cộng điểm
        Debug.Log("Current Score: " + currentScore); // Ghi log điểm
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
        Application.Quit(); // Thoát game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Dừng game trong Editor
#endif
    }

    public void RestartGame()
    {
        if (click != null)
        {
            audioSource.PlayOneShot(click);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Tải lại scene hiện tại

    }

    public void PauseGame()
    {
        // Dừng hoặc tiếp tục game
        if (Time.timeScale == 1)
        {
            if (click != null)
        {
            audioSource.PlayOneShot(click);
        }
            Time.timeScale = 0; // Dừng game
            ShowHidePanel(true); // Hiện panel
            PauseButton.SetActive(false);
        }
        else
        {
            if (click != null)
        {
            audioSource.PlayOneShot(click);
        }
            Time.timeScale = 1; // Tiếp tục game
            ShowHidePanel(false); // Ẩn panel
            PauseButton.SetActive(true);
        }
    }
    public void changeMusic(){
        showChangeMusicPanel(ChangeMusicPannel);
    }
    private void showChangeMusicPanel(GameObject panel){
        panel.SetActive(!panel.activeSelf); // Đảo ngược trạng thái hiện tại
    }

    private void ShowHidePanel(bool isVisible)
    {
        PausePannel.SetActive(isVisible); // Hiện hoặc ẩn panel
    }

    public void LoadSceneByName(string sceneName)
    {
        // Kiểm tra xem scene có tồn tại không
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
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Tải điểm cao nhất, mặc định là 0 nếu chưa có
        highScoreDate = PlayerPrefs.GetString("HighScoreDate", "Chưa có"); // Tải ngày đạt điểm cao nhất
        Debug.Log("Điểm cao nhất hiện tại: " + highScore + " vào lúc " + highScoreDate);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore); // Lưu điểm cao nhất
        PlayerPrefs.SetString("HighScoreDate", highScoreDate); // Lưu ngày đạt điểm cao nhất
        PlayerPrefs.Save(); // Lưu các thay đổi
        Debug.Log("Lưu điểm cao nhất: " + highScore + " vào lúc " + highScoreDate);
    }
}