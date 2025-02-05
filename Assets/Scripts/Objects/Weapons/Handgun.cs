using Unity.VisualScripting;
using UnityEngine;

public class Handgun : WeaponBase
{
    public float ShootingSpeed = 5;

    private float elpTime = 0;

    public float rayTime = 0.03f;

    public Color rayColor = Color.red;

    public Transform shootOffset;

    public GunSoundPlayer soundPlayer;

    public void Start()
    {
        elpTime = 0;

        soundPlayer = Utils.GetListOfType<GunSoundPlayer>()[0];
    }

    public void Update()
    {
        elpTime += Time.deltaTime;
    }

    public override void Use()
    {
        if(elpTime > ShootingSpeed)
        {
            soundPlayer.PlaySound();

            elpTime = 0;

            Ray ray = new Ray (shootOffset.position, -transform.right);
            RaycastHit hit;

            if (Physics.Raycast (ray, out hit, 100))
            {
                Utils.DrawRay(shootOffset.transform.position, -transform.right * Vector3.Distance(shootOffset.position, hit.point), rayColor, rayTime);

                var player = hit.collider.gameObject.GetComponent<PlayerHealth>(); 
                if(player != null)
                {
                    player.ApplyDamage(Damage);
                }

                var enemy = hit.collider.gameObject.GetComponent<EnemyBase>(); 
                if(enemy != null)
                {
                    enemy.ApplyDamage(Damage);
                }
            }
            else 
            {
                Utils.DrawRay(shootOffset.transform.position, -transform.right, rayColor, rayTime);
            }
        }
    }
}
