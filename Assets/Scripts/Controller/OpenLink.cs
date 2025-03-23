using UnityEngine;

public class OpenLink : MonoBehaviour
{
    // Method to open URL
    public void OpenURL(string url)
    {
        Application.OpenURL(url); // Opens the specified URL in the default web browser
    }
}