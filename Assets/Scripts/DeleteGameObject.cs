using UnityEngine;
using UnityEngine.EventSystems;

public class DestroySelf : MonoBehaviour, IPointerClickHandler
{
    // Hàm này sẽ xóa game object sở hữu script này
    public AudioSource bublePop;
    public void DestroyThis()
    {
        bublePop.Play();
        Debug.Log("Amthanh");
        Destroy(gameObject);
    }

    // Phương thức này sẽ được gọi khi game object bị nhấp chuột
    public void OnPointerClick(PointerEventData eventData)
    {
        bublePop.Play();
        DestroyThis(); // Gọi hàm để xóa game object
    }
}