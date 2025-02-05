using System;
using UnityEngine;

public class SimpleWaves : MonoBehaviour
{
    public Transform[] Floaters;
    public float WavePower = 15f;
    public float WaterHeight = 0f;

    Rigidbody Rb;
    bool Underwater;
    int FloatersUnderWater;

    // Start is called before the first frame update
    void Start()
    {
        Rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FloatersUnderWater = 0;
        for(int i = 0; i < Floaters.Length; i++)
        {
            float diff = Floaters[i].position.y - WaterHeight;
            if (diff < 0)
            {
                Rb.AddForceAtPosition(Vector3.up * ((float)Math.Sin(Floaters[i].position.x * Time.deltaTime * 1000) + 1) * WavePower, Floaters[i].position, ForceMode.Force);
                FloatersUnderWater += 1;
            }
        }
    }
}
