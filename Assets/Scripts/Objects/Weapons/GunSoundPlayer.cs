
using UnityEngine;

public class GunSoundPlayer : MonoBehaviour
{
    private AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }
    
    public void PlaySound()
    {
        audioData.Play(0);
    }
}