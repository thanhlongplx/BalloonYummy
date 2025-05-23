using TMPro;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource; // Tham chiếu đến AudioSource đã có
    public TextMeshProUGUI musicStatusText; // Tham chiếu đến TextMeshPro cho trạng thái âm nhạc

    private const string MusicPrefKey = "MusicEnabled"; // Khóa để lưu trạng thái âm nhạc

    void Start()
    {
        // Đảm bảo AudioSource không phát tự động
        musicSource.enabled = PlayerPrefs.GetInt(MusicPrefKey, 1) == 1; // Mặc định là bật
        UpdateMusicStatus(); // Cập nhật trạng thái ban đầu cho nút
        
    }

    public void ToggleMusic()
    {
        if (musicSource != null)
        {
            // Chuyển đổi trạng thái hoạt động của AudioSource
            musicSource.enabled = !musicSource.enabled; // Bật hoặc tắt AudioSource

            // Nếu bật, phát âm thanh
            if (musicSource.enabled)
            {
                musicSource.Play(); // Phát âm thanh nếu bật
                Debug.Log("Music is playing"); // Thông báo trong Console
            }
            else
            {
                musicSource.Stop(); // Dừng âm thanh nếu tắt
                Debug.Log("Music is stopped"); // Thông báo trong Console
            }

            // Lưu trạng thái vào PlayerPrefs
            PlayerPrefs.SetInt(MusicPrefKey, musicSource.enabled ? 1 : 0);
            PlayerPrefs.Save(); // Lưu thay đổi

            // Cập nhật trạng thái cho nút
            UpdateMusicStatus();
        }
        else
        {
            Debug.LogError("MusicSource is not assigned!"); // Thông báo lỗi nếu musicSource không được gán
        }
    }

    private void UpdateMusicStatus()
    {
        // Cập nhật nội dung của TextMeshPro dựa trên trạng thái âm nhạc
        musicStatusText.text = musicSource.enabled ? "On" : "Off"; // Đổi trạng thái cho đúng
    }
}