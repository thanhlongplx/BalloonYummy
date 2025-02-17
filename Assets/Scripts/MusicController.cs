using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource[] musicSources; // Array to hold audio sources

    void Start()
    {
        musicSources = new AudioSource[2]; // Initialize the array for two audio sources
        for (int i = 0; i < musicSources.Length; i++)
        {
            musicSources[i] = gameObject.AddComponent<AudioSource>(); // Add AudioSource
            musicSources[i].playOnAwake = true; // Enable Play On Awake
        }

        // Assign audio clips directly in the Inspector (done in Unity)
    }

    public void ToggleMusic(int trackIndex, bool isOn)
    {
        if (trackIndex >= 0 && trackIndex < musicSources.Length)
        {
            musicSources[trackIndex].mute = !isOn; // Mute or unmute based on the toggle
        }
    }

    void Update()
    {
        // Example key binds to toggle music tracks
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Press 1 to toggle the first track
        {
            ToggleMusic(0, !musicSources[0].mute); // Toggle first track
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // Press 2 to toggle the second track
        {
            ToggleMusic(1, !musicSources[1].mute); // Toggle second track
        }
    }
}