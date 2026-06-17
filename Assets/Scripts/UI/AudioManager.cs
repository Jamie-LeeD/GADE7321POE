using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip buttonClickSFX;

    private readonly List<Button> wiredButtons = new List<Button>();

    private void Start()
    {
        StopMusicFromOtherManagers();
        PlayMusic(backgroundMusic);
        WireButtonClickSounds();
    }

    private void OnDestroy()
    {
        UnwireButtonClickSounds();
        StopMusic();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource == null) return;

        musicSource.Stop();
        musicSource.clip = null;
    }

    private void StopMusicFromOtherManagers()
    {
        AudioManager[] managers = Object.FindObjectsByType<AudioManager>(FindObjectsSortMode.None);

        foreach (AudioManager manager in managers)
        {
            if (manager != this)
            {
                manager.StopMusic();
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.PlayOneShot(clip);
    }

    public void PlayButtonClick()
    {
        if (SfxManager.Instance != null)
        {
            SfxManager.Instance.PlaySound(SfxKeys.UiButtonClick);
            return;
        }

        PlaySFX(buttonClickSFX);
    }

    private void WireButtonClickSounds()
    {
        Button[] buttons = Object.FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Button button in buttons)
        {
            if (button == null)
            {
                continue;
            }

            button.onClick.AddListener(PlayButtonClick);
            wiredButtons.Add(button);
        }
    }

    private void UnwireButtonClickSounds()
    {
        foreach (Button button in wiredButtons)
        {
            if (button != null)
            {
                button.onClick.RemoveListener(PlayButtonClick);
            }
        }

        wiredButtons.Clear();
    }
}
