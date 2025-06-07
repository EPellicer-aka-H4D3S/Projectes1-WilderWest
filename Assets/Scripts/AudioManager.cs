using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource EffectSource;

    public AudioClip music;
    public AudioClip jump;
    public AudioClip nut;
    public AudioClip hit;
    public AudioClip death;

    void Start()
    {
        MusicSource.clip = music;
        MusicSource.Play();
    }

    void Update()
    {
        if (Time.timeScale == 0.0f)
        {
            MusicSource.Pause();
        }
        else if(!MusicSource.isPlaying)
        {
            MusicSource.Play();
        }
    }

    public void playEffect(AudioClip clip)
    {
        EffectSource.PlayOneShot(clip);
    }
}
