using UnityEngine;
using System;

public class BlockPushAudioEventForwarder : MonoBehaviour
{
    public static event Action<Vector2Int> OnAnyBlockPushSuccess;

    /// <summary>
    /// Receives the successful move message from Block and forwards it as a global push-audio event.
    /// </summary>
    private void BlockMoved(Vector2Int dir)
    {
        OnAnyBlockPushSuccess?.Invoke(dir);
    }
}