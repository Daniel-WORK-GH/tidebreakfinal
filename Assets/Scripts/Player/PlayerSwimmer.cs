using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSwimmer : MonoBehaviour
{
    public float waterLevel = 28;

    public float swimForce = 1;

    public VolumeProfile volumeProfile;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && transform.position.y <= waterLevel)
        {
            rb.AddForce(transform.up * swimForce, ForceMode.Impulse);
        }

        if(transform.position.y <= waterLevel - 0.4f)
        {
            volumeProfile.components[0].active = true;
        }
        else 
        {
            volumeProfile.components[0].active = false;
        }
    }
}
