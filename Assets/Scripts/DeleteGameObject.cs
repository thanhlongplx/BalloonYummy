using UnityEngine;
using UnityEngine.EventSystems;

public class DestroySelf : MonoBehaviour, IPointerClickHandler
{
    // This function will delete object got this script 
    public AudioSource bublePop;
    public void DestroyThis()
    {
        bublePop.Play();
        
        Destroy(gameObject);
    }

    // This function will be call when game object was clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        bublePop.Play();
        DestroyThis(); // Call the function to destroy game object
    }
}