using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private BaseAudioManager audioManager;
    [SerializeField] private AudioClip[] footClips;
    
    public void PlayFootStepSound()
    {
        audioManager.PlaySound(footClips[Random.Range(0, footClips.Length)]);
    }
}
