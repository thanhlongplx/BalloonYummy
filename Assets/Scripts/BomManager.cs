using UnityEngine;

public class BomManager : MonoBehaviour
{
    public GameObject[] BoomPrefabs; // Mảng chứa các prefab bom
    public float throwForce = 20f; // Lực ném
    public Vector2 spawnRangeX; // Khoảng giới hạn x cho vị trí spawn
    public float spawnPosY; // Vị trí y để spawn
    public float timeSpawn = 4f; // Thời gian giữa các lần spawn
    public ParticleSystem explosionEffect; // Tham chiếu đến Particle System
    public AudioClip bubblePopClip; // Clip âm thanh nổ bong bóng
    public AudioClip boomClip; // Clip âm thanh nổ bom

    private float m_timeSpawn; // Thời gian còn lại cho lần spawn tiếp theo

    void Start()
    {
        m_timeSpawn = 0;
        // Gọi phương thức để ném đối tượng lần đầu tiên
        ThrowObject();
    }

    void Update()
    {
        // Giảm thời gian còn lại để spawn
        m_timeSpawn -= Time.deltaTime;
        if (m_timeSpawn <= 0)
        {
            SpawnBom();
            m_timeSpawn = timeSpawn; // Đặt lại thời gian spawn
        }
        // Kiểm tra nếu tọa độ Y vượt quá maxYPosition
        if (transform.position.y < -12f)
        {
            Destroy(gameObject); // Xóa bong bóng này
        }
    }

    void ThrowObject()
    {
        // Tạo vị trí ngẫu nhiên trong khoảng giới hạn
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        Vector2 spawnPos = new Vector2(randomX, spawnPosY);

        // Chọn ngẫu nhiên một prefab từ mảng
        if (BoomPrefabs.Length > 0)
        {
            GameObject randomBoomPrefab = BoomPrefabs[Random.Range(0, BoomPrefabs.Length)];
            GameObject boomObject = Instantiate(randomBoomPrefab, spawnPos, Quaternion.identity);

            // Lấy Rigidbody2D và thêm lực ném
            Rigidbody2D rb = boomObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 throwDirection = new Vector2(0, 1); // Ném từ dưới lên
                rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse); // Thêm lực ném
            }

            // Gán hiệu ứng nổ và âm thanh cho bom
            Boom boomScript = boomObject.AddComponent<Boom>();
            boomScript.explosionEffect = explosionEffect; // Gán hiệu ứng nổ
            boomScript.bubblePopClip = bubblePopClip; // Gán âm thanh nổ
            boomScript.boomClip = boomClip; // Gán âm thanh khác
        }
    }

    public void SpawnBom()
    {
        // Gọi phương thức ném đối tượng
        ThrowObject();
    }
}