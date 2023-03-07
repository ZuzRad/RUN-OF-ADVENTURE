using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public enum Sounds
    {
        Win, Hit, Jump, Money

    }
    [Header("Audio")]
    public AudioClip hitAudio;
    public AudioClip stage1MusicAudio;
    public AudioClip stage2MusicAudio;
    public AudioClip stage3MusicAudio;
    public AudioClip stage4MusicAudio;
    public AudioClip stagePauseMusicAudio;
    public AudioClip ShopMusicAudio;
    public AudioClip menuMusicAudio;
    public AudioClip tutorialMusicAudio;
    public AudioClip moneyAudio;
    public AudioClip jumpAudio;
    public AudioClip winAudio;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
        if (SceneManager.GetActiveScene().buildIndex == 0)
            audioSource.PlayOneShot(menuMusicAudio);

        else if (SceneManager.GetActiveScene().buildIndex == 1)
            audioSource.PlayOneShot(ShopMusicAudio);

        else if (SceneManager.GetActiveScene().buildIndex == 2)
            audioSource.PlayOneShot(stagePauseMusicAudio);

        else if (SceneManager.GetActiveScene().buildIndex == 3)
            audioSource.PlayOneShot(stage1MusicAudio);

        else if (SceneManager.GetActiveScene().buildIndex == 4)
            audioSource.PlayOneShot(stage2MusicAudio);

        else if (SceneManager.GetActiveScene().buildIndex == 5)
            audioSource.PlayOneShot(stage3MusicAudio);

        else if (SceneManager.GetActiveScene().buildIndex == 6)
            audioSource.PlayOneShot(stage4MusicAudio);

        else if (SceneManager.GetActiveScene().buildIndex == 7)
            audioSource.PlayOneShot(tutorialMusicAudio);
    }


    public void PlaySound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.Jump:
                audioSource.loop = true;
                audioSource.PlayOneShot(jumpAudio);
                break;

            case Sounds.Win:
                audioSource.loop = true;
                audioSource.PlayOneShot(winAudio);
                break;

            case Sounds.Hit:
                audioSource.loop = true;
                audioSource.PlayOneShot(hitAudio);
                break;

            case Sounds.Money:
                audioSource.loop = true;
                audioSource.PlayOneShot(moneyAudio);
                break;

            default:
                break;
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
