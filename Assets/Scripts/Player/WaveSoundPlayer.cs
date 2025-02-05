using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public float waterLevel = 28;
    public float maxWaterSoundsLevel = 28.5f;

    private AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        Debug.Log("started");
    }

    void Update()
    {
        Ray ray = new Ray (transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast (ray, out hit, 1.1f))
        {
            if(transform.position.y > maxWaterSoundsLevel)
            {
                if(hit.collider.gameObject.name == "Ground")
                {
                    audioData.volume = 0.1f;
                }
                else 
                {
                    audioData.volume = 1;
                }
            }
            else 
            {
                audioData.volume = 1;
            }
        }
    }
}
