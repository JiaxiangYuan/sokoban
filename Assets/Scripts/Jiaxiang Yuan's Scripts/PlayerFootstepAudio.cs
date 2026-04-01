using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootstepAudio : MonoBehaviour
{
    [SerializeField] private AudioClip stepClipA;
    [SerializeField] private AudioClip stepClipB;

    private AudioSource audioSource;
    private bool playFirst = true;

    /// <summary>
    /// Caches AudioSource and configures playback settings.
    /// </summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    /// <summary>
    /// Receives the BlockMoved broadcast and plays alternating footstep sounds.
    /// </summary>
    private void BlockMoved(Vector2Int dir)
    {
        AudioClip clip = playFirst ? stepClipA : stepClipB;

        if (clip == null) return;

        audioSource.PlayOneShot(clip);
        playFirst = !playFirst;
    }
}