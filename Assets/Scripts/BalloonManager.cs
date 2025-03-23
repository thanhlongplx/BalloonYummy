
using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    public GameObject[] balloonPrefabs; // Mảng chứa các prefab bong bóng và quả bom
    public float timeSpawn = 0.2f;
    public float moveSpeed = 2f; // Tốc độ bay lên
    public Vector2 spawnRangeX = new Vector2(-3, 3); // Khoảng vị trí X
    public float spawnPosY = -6; // Vị trí Y cố định
    private float m_timeSpawn;

    void Start()
    {
        m_timeSpawn = 0;
    }

    void Update()
    {
        m_timeSpawn -= Time.deltaTime;
        if (m_timeSpawn <= 0)
        {
            SpawnBalloon();
            m_timeSpawn = timeSpawn;
        }
    }

    public void SpawnBalloon()
    {
        Vector2 spawnPos = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), spawnPosY);
        if (balloonPrefabs.Length > 0)
        {
            // Chọn ngẫu nhiên một prefab từ mảng
            GameObject randomBalloon = balloonPrefabs[Random.Range(0, balloonPrefabs.Length)];
            GameObject balloon = Instantiate(randomBalloon, spawnPos, Quaternion.identity);

            // Thiết lập tag dựa trên prefab
            if (randomBalloon.CompareTag("Boom"))
            {
                balloon.tag = "Boom"; // Nếu là quả bom
            }
            else
            {
                balloon.tag = "Balloon"; // Nếu là bong bóng
            }

            // Nếu cần, bạn có thể thiết lập tốc độ di chuyển nếu đã có script BubbleMovement trong prefab
            BubbleMovement bubbleMovement = balloon.GetComponent<BubbleMovement>();
            if (bubbleMovement != null)
            {
                bubbleMovement.moveSpeed = moveSpeed; // Thiết lập tốc độ di chuyển
            }
        }
    }
}