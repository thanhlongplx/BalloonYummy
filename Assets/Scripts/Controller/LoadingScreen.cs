using UnityEngine;
using UnityEngine.UI; // Để sử dụng các phần tử UI
using UnityEngine.SceneManagement;
using System.Collections; // Để load scene

public class LoadingScreen : MonoBehaviour
{

    public Image loadingBar; // Tham chiếu đến phần tử UI thanh load
    private float currentLoad = 0f; // Tiến trình tải hiện tại
    public string sceneNameLoad;
    public void Start()
    {
        Time.timeScale = 1;

        StartLoading(sceneNameLoad);

    }

    // Hàm này được gọi từ các scene khác để bắt đầu quá trình tải
    public void StartLoading(string sceneName) // Mặc định là "Menu"
    {
        if (sceneName == "Menu")
        {
            Debug.Log("Load bắt đầu load scene" + sceneName);
            StartCoroutine(LoadAsyncScene("Menu"));

        }

        // Reset tiến trình tải
        currentLoad = 0f;
        StartCoroutine(LoadAsyncScene(sceneName));
    }

    private IEnumerator LoadAsyncScene(string sceneName)
    {
        // Bắt đầu tải scene không đồng bộ
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // Cập nhật thanh load cho đến khi hoàn tất
        while (!operation.isDone)
        {
            // Cập nhật tiến trình tải
            currentLoad += 0.3f * Time.deltaTime / 2f; // Tăng tiến trình tải (thay đổi giá trị để điều chỉnh tốc độ)
            UpdateLoadingBar(currentLoad);

            // Khi tiến trình đạt 90%, cho phép chuyển đến scene
            if (currentLoad >= 1f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null; // Đợi cho frame tiếp theo
        }
    }

    private void UpdateLoadingBar(float loadProgress)
    {
        // Cập nhật lượng thanh load dựa trên tiến trình tải hiện tại
        loadingBar.fillAmount = loadProgress; // Lượng cần ở giữa 0 và 1
    }
}