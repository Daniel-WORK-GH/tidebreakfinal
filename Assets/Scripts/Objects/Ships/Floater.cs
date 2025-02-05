using UnityEngine;

public class Floater : MonoBehaviour
{
    public Transform[] Floaters;
    public float UnderWaterDrag = 3f;
    public float UnderWaterAngularDrag = 1f;
    public float AirDrag = 0f;
    public float AirAngularDrag = 0.05f;
    public float FloatingPower = 15f;
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
                Rb.AddForceAtPosition(Vector3.up * FloatingPower * Mathf.Abs(diff), Floaters[i].position, ForceMode.Force);
                FloatersUnderWater += 1;
                if (!Underwater)
                {
                    Underwater = true;
                    SwitchState(true);
                }
            }
        }
        if (Underwater && FloatersUnderWater==0)
        {
            Underwater = false;
            SwitchState(false);
        }
    }

    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            Rb.linearDamping = UnderWaterDrag;
            Rb.angularDamping = UnderWaterAngularDrag;
        }
        else
        {
            Rb.linearDamping = AirDrag;
            Rb.angularDamping = AirAngularDrag;
        }
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach(Transform f in Floaters)
        {
            Gizmos.DrawWireCube(f.transform.position, Vector3.one);
        }
    }
}
