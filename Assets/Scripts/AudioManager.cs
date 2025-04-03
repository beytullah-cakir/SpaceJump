using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public Sound[] sfxSounds,
       musicSounds;
   public AudioSource sfxSource,
       musicSource;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic("MainTheme");
    }
    public void PlaySFX(string name)
    {
        Sound sfx = System.Array.Find(sfxSounds, sound => sound.name == name);
        if (sfx != null)
        {
            sfxSource.PlayOneShot(sfx.clip);
        }
    }

    public void PlayMusic(string name)
    {
        Sound music = System.Array.Find(musicSounds, sound => sound.name == name);
        if (music != null)
        {
            musicSource.clip = music.clip;
            musicSource.Play();
        }
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }

}
