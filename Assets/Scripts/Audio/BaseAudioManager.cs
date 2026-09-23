using UnityEngine;

public class BaseAudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
