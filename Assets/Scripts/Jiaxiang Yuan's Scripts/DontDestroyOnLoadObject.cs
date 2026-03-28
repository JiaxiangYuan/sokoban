using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    private static DontDestroyOnLoadObject instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}