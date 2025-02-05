using Unity.Mathematics;
using UnityEngine;

public class Sword : WeaponBase
{
    public float attackSpeed = 1;

    public float startRotationX = 0;
    public float endRotationX = 90;

    private float elpTime = 0;

    private bool swing = false;
    

    public void Start()
    {
        elpTime = attackSpeed;
    }

    public void Update()
    {
        elpTime += Time.deltaTime;

        if(swing)
        {
            if(elpTime < 0.5 * attackSpeed)
            {
                float r = math.lerp(startRotationX, endRotationX, elpTime * 2);
                transform.localRotation = Quaternion.Euler(r, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            else 
            {
                float r = math.lerp(endRotationX, startRotationX, elpTime * 2);
                transform.localRotation = Quaternion.Euler(r, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
        }

        if(elpTime > attackSpeed)
        {
            swing = false;
            transform.localRotation = Quaternion.Euler(300, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }

    public override void Use()
    {
        if(elpTime > attackSpeed && !swing)
        {
            elpTime = 0;
            swing = true;
        }
    }
}
