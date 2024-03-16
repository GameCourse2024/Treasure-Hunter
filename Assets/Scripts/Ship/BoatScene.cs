using UnityEngine;

public class BoatScene : MonoBehaviour
{
    [SerializeField] private AudioManagerGamePlay audioManager;
    [SerializeField] private string themeSound;

    // Start is called before the first frame update
    void Start()
    {
        if (audioManager == null)
        {
            Debug.LogWarning("AudioManagerGamePlay reference not set in BoatScene.");
            return;
        }

        AudioManagerGamePlay.Instance.Play(themeSound);

    }
}
