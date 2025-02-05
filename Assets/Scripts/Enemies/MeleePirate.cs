using Unity.VisualScripting;
using UnityEngine;

public class MeleePirate : EnemyBase
{
    public WeaponBase weapon;
    public float attackRange = 5;

    void Start()
    {
        
    }

    new void Update()
    {
        base.Update();

        SelectPlayerFocus();

        if(currentFocusPlayer != null)
        {
            LookAtPlayer(currentFocusPlayer);

            if(Vector3.Distance(currentFocusPlayer.transform.position, transform.position) <= attackRange)
            {
                weapon.Use();
                weapon.ApplyDamage(currentFocusPlayer.GetComponent<PlayerHealth>());
            }
            else
            {
                Vector3 direction = (currentFocusPlayer.transform.position - transform.position).normalized;

                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
    }

    new void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
