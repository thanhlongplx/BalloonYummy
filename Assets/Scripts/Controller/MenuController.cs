using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Nhập thư viện TextMeshPro


public class MenuController : MonoBehaviour
{


    public AudioClip click;
    public TextMeshProUGUI highScoreText; // Tham chiếu đến TextMeshPro
    public TextMeshProUGUI highScoreDateText; // Tham chiếu đến TextMeshPro cho thời gian
    public GameObject highScorePanel;
    private AudioSource audioSource; // AudioSource để phát âm thanh


    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Thêm AudioSource
        LoadAndDisplayHighScore(); // Gọi hàm để tải và hiển thị điểm cao nhất

    }

    void Update()
    {



    }





    public void ExitGame()
    {
        Application.Quit(); // Thoát game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Dừng game trong Editor
#endif
    }





    public void LoadSceneByName(string sceneName)
    {
        if (click != null)
        {
            audioSource.PlayOneShot(click);
        }
        // Kiểm tra xem scene có tồn tại không
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene không tồn tại: " + sceneName);
        }
    }
    public void PauseGame()
    {
        // Dừng hoặc tiếp tục game

        if (click != null)
        {
            audioSource.PlayOneShot(click);
        }
        Time.timeScale = 1; // Tiếp tục game


    }
    public void TogglePanel(GameObject panel)
    {
        if (click != null)
        {
            audioSource.PlayOneShot(click);
        }
        panel.SetActive(!panel.activeSelf); // Đảo ngược trạng thái hiện tại
    }
    private void LoadAndDisplayHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Tải điểm cao nhất, mặc định là 0
        string highScoreDate = PlayerPrefs.GetString("HighScoreDate", "Chưa có"); // Tải ngày đạt điểm cao nhất
        highScoreText.text = "High Score: " + highScore.ToString(); // Cập nhật nội dung TextMeshPro
        highScoreDateText.text = highScoreDate; // Cập nhật thời gian
    }
}