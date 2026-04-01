using UnityEngine;

[RequireComponent(typeof(Slidey))]
[RequireComponent(typeof(AudioSource))]
public class SlideyAudio : MonoBehaviour
{
    [SerializeField] private AudioClip slideStepClip;
    [SerializeField] private AudioClip slideStopClip;

    private AudioSource audioSource;
    private Block block;
    private bool wasMoving;
    private bool isStepPlaying;

    /// <summary>
    /// Initializes component references and configures the AudioSource.
    /// </summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        block = GetComponent<Block>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        isStepPlaying = false;
    }

    /// <summary>
    /// Sets the initial movement state when the object becomes active.
    /// </summary>
    private void OnEnable()
    {
        wasMoving = block != null && block.State == Block.MoveStates.moving;
    }

    /// <summary>
    /// Monitors movement state changes to stop sliding audio immediately
    /// and trigger the stop sound when movement ends.
    /// </summary>
    private void Update()
    {
        if (block == null) return;

        bool isMoving = block.State == Block.MoveStates.moving;

        if (wasMoving && !isMoving)
        {
            StopSlideSoundImmediately();
            PlayStopSound();
        }

        wasMoving = isMoving;
    }

    /// <summary>
    /// Called when the block moves; attempts to play the sliding sound.
    /// </summary>
    private void BlockMoved(Vector2Int dir)
    {
        TryPlaySlideStepSound();
    }

    /// <summary>
    /// Plays the slide step sound once if it is not already playing.
    /// </summary>
    private void TryPlaySlideStepSound()
    {
        if (slideStepClip == null) return;

        if (isStepPlaying) return;

        audioSource.clip = slideStepClip;
        audioSource.Play();

        isStepPlaying = true;
    }

    /// <summary>
    /// Stops the current slide sound immediately and resets the playback flag.
    /// </summary>
    private void StopSlideSoundImmediately()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        isStepPlaying = false;
    }

    /// <summary>
    /// Plays the stop/impact sound when sliding ends.
    /// </summary>
    private void PlayStopSound()
    {
        if (slideStopClip == null) return;

        audioSource.PlayOneShot(slideStopClip);
    }
}