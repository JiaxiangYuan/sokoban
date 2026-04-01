using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GlobalBlockPushSuccessAudio : MonoBehaviour
{
    [SerializeField] private AudioClip pushSuccessClip;

    private AudioSource audioSource;

    /// <summary>
    /// Caches the AudioSource component.
    /// </summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Subscribes to the global block push success audio event.
    /// </summary>
    private void OnEnable()
    {
        BlockPushAudioEventForwarder.OnAnyBlockPushSuccess += HandleAnyBlockPushSuccess;
    }

    /// <summary>
    /// Unsubscribes from the global block push success audio event.
    /// </summary>
    private void OnDisable()
    {
        BlockPushAudioEventForwarder.OnAnyBlockPushSuccess -= HandleAnyBlockPushSuccess;
    }

    /// <summary>
    /// Plays the global push success sound when any forwarded block push succeeds.
    /// </summary>
    private void HandleAnyBlockPushSuccess(Vector2Int dir)
    {
        if (pushSuccessClip == null) return;
        audioSource.PlayOneShot(pushSuccessClip);
    }
}