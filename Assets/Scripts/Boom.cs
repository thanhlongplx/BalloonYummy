using UnityEngine;

public class Boom : MonoBehaviour
{
    public ParticleSystem explosionEffect; // Tham chiếu đến hiệu ứng nổ
    public AudioClip bubblePopClip; // Clip âm thanh
    public AudioClip boomClip; // Clip âm thanh
    private SpriteRenderer spriteRenderer; // Tham chiếu đến SpriteRenderer
    private GameController gameController;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer
        gameController = FindObjectOfType<GameController>();
    }

    private void OnMouseDown()
    {
         Debug.Log("Tag của GameObject: " + gameObject.tag);
        // Tạo hiệu ứng nổ tại vị trí của bom
        if (explosionEffect != null)
        {
            ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(effect.gameObject, effect.main.duration); // Xóa hiệu ứng sau khi phát xong
        }

        // Phát âm thanh
        if (bubblePopClip != null)
        {
            AudioSource.PlayClipAtPoint(bubblePopClip, transform.position);
        }
        if (boomClip != null)
        {
            AudioSource.PlayClipAtPoint(boomClip, transform.position);
        }

        // Cập nhật điểm số
        if (gameController.currentScore >= 20)
        {
            gameController.AddScore(gameController.Level - 1); // Cộng 2 điểm mỗi khi bom bị nổ
        }
        else
        {
            gameController.AddScore(1); // Cộng 1 điểm mỗi khi bom bị nổ
        }

        // Xóa đối tượng bom
        Destroy(gameObject); // Xóa bom
    }
}