using System.Collections;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{


    public float moveSpeed = 2f; // Tốc độ di chuyển
    public float maxYPosition = 5f; // Mức Y tối đa để xóa bong bóng
    public AudioClip bubblePopClip; // Clip âm thanh
    public AudioClip BoomClip; // Clip âm thanh
    public ParticleSystem explosionEffect; // Tham chiếu đến Particle System
    private SpriteRenderer spriteRenderer; // Tham chiếu đến SpriteRenderer
    GameController gameController;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        // Di chuyển bong bóng lên theo trục Y
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        // Kiểm tra nếu tọa độ Y vượt quá maxYPosition
        if (transform.position.y > maxYPosition)
        {
            Destroy(gameObject); // Xóa bong bóng này
        }






    }

    void OnMouseDown()
    {
        // Ghi lại tag của GameObject
        Debug.Log("Tag của GameObject: " + gameObject.tag);

        // Phát âm thanh và bắt đầu quá trình biến mất
        PlaySoundAndTriggerEffects();

        Debug.Log(gameController.currentScore);
    }

    private void PlaySoundAndTriggerEffects()
    {
        // Tạo AudioSource tạm thời để phát âm thanh
        AudioSource tempAudioSource1 = gameObject.AddComponent<AudioSource>();
        AudioSource tempAudioSource2 = gameObject.AddComponent<AudioSource>();
        tempAudioSource1.clip = bubblePopClip; // Gán clip âm thanh
        tempAudioSource2.clip = BoomClip; // Gán clip âm thanh
        tempAudioSource1.Play(); // Phát âm thanh
        tempAudioSource2.Play(); // Phát âm thanh

        // Phát Particle System
        if (explosionEffect != null)
        {
            ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            // Random màu sắc cho Particle System
            var main = effect.main;
            main.startColor = new ParticleSystem.MinMaxGradient(GetRandomColor()); // Gán màu sắc ngẫu nhiên
            effect.Play(); // Phát hiệu ứng nổ

            Destroy(effect.gameObject, effect.main.duration); // Xóa Particle System sau khi phát xong
        }

        StartCoroutine(FadeOutAndDestroy(tempAudioSource1)); // Bắt đầu coroutine để biến mất và xóa
        StartCoroutine(FadeOutAndDestroy(tempAudioSource2)); // Bắt đầu coroutine để biến mất và xóa
    }

    private Color GetRandomColor()
    {
        // Tạo màu sắc ngẫu nhiên
        return new Color(Random.value, Random.value, Random.value);
    }

    private IEnumerator FadeOutAndDestroy(AudioSource audioSource)
    {
        // Biến mất dần dần
        Color originalColor = spriteRenderer.color;
        float fadeDuration = audioSource.clip.length; // Thời gian biến mất tương đương với thời gian âm thanh

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - normalizedTime); // Giảm độ trong suốt
            yield return null; // Chờ một frame
        }

        // Đảm bảo rằng bong bóng hoàn toàn trong suốt
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        if (gameController.currentScore >= 20)
        {
            gameController.AddScore(gameController.Level - 1); // Cộng 2 điểm mỗi khi bong bóng bị nổ
        }
        else
        {
            gameController.AddScore(1); // Cộng 1 điểm mỗi khi bong bóng bị nổ
        }


        // Xóa AudioSource sau khi âm thanh phát xong
        Destroy(audioSource);

        Destroy(gameObject); // Xóa bong bóng
    }
}