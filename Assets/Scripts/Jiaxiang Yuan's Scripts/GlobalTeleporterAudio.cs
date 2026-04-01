using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GlobalTeleporterAudio : MonoBehaviour
{
    [SerializeField] private AudioClip teleportClip;

    private AudioSource audioSource;

    /// <summary>
    /// Caches the AudioSource and configures its playback settings.
    /// </summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    /// <summary>
    /// Subscribes to the global teleporter event.
    /// </summary>
    private void OnEnable()
    {
        Teleporter.OnAnyTeleportHappened += HandleAnyTeleportHappened;
    }

    /// <summary>
    /// Unsubscribes from the global teleporter event.
    /// </summary>
    private void OnDisable()
    {
        Teleporter.OnAnyTeleportHappened -= HandleAnyTeleportHappened;
    }

    /// <summary>
    /// Plays the shared teleport sound whenever any teleporter completes a teleport.
    /// </summary>
    private void HandleAnyTeleportHappened()
    {
        if (teleportClip == null) return;

        audioSource.PlayOneShot(teleportClip);
    }
}