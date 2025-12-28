using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public AudioSource source;
    public AudioClip[] audios = new AudioClip[3];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    public void AuidoStart(int value)
    {
        source.PlayOneShot(audios[value]);
    }
}
