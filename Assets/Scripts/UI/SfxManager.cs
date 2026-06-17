using UnityEngine;

public class SfxManager : MonoBehaviour, ISimpleListener
{
    [System.Serializable]
    public class SfxEntry
    {
        public string key;
        public AudioClip clip;
    }

    public static SfxManager Instance { get; private set; }
    public AudioSource sfxSource;
    public SfxEntry[] soundLibrary;

    private MyHashMap<string, AudioClip> soundMap;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        soundMap = new MyHashMap<string, AudioClip>();
        RegisterSounds();
    }

    void Start()
    {
        if (SimpleEventBus.Instance != null)
        {
            SimpleEventBus.Instance.AddListener(GameEventType.PlaySfx, this);
            SimpleEventBus.Instance.AddListener(GameEventType.Jump, this);
            SimpleEventBus.Instance.AddListener(GameEventType.GameOver, this);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }

        if (SimpleEventBus.Instance != null)
        {
            SimpleEventBus.Instance.RemoveListener(GameEventType.PlaySfx, this);
            SimpleEventBus.Instance.RemoveListener(GameEventType.Jump, this);
            SimpleEventBus.Instance.RemoveListener(GameEventType.GameOver, this);
        }
    }

    private void RegisterSounds()
    {
        if (soundLibrary == null)
        {
            return;
        }

        foreach (SfxEntry entry in soundLibrary)
        {
            if (entry == null || string.IsNullOrEmpty(entry.key) || entry.clip == null)
            {
                continue;
            }

            soundMap.Add(entry.key, entry.clip);
        }
    }

    public void PlaySound(string key)
    {
        if (string.IsNullOrEmpty(key) || sfxSource == null || soundMap == null)
        {
            return;
        }

        if (soundMap.TryGetValue(key, out AudioClip clip) && clip != null)
        {
            sfxSource.PlayOneShot(clip);
            return;
        }

        Debug.LogWarning("No AudioClip registered for key '{key}'.");
    }

    public void OnEvent(GameEventType eventType, object sender, object param = null)
    {
        if (eventType == GameEventType.PlaySfx)
        {
            PlaySound(param as string);
        }
        else if (eventType == GameEventType.Jump)
        {
            PlaySound(SfxKeys.Jump);
        }
        else if (eventType == GameEventType.GameOver)
        {
            PlaySound(SfxKeys.PlayerDeath);
        }
    }
}
