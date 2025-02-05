using UnityEngine;

public class RangedPirate : EnemyBase
{
    public WeaponBase weapon;

    void Start()
    {
        
    }

    new void Update()
    {
            base.Update();

        SelectPlayerFocus();

        if(currentFocusPlayer != null)
        {
            if(Vector3.Distance(currentFocusPlayer.transform.position, transform.position) < lockRange)
            {
                LookAtPlayer(currentFocusPlayer);
            
                weapon.Use();
            }
            else 
            {
                Vector3 direction = (currentFocusPlayer.transform.position - transform.position).normalized;

                transform.position += direction * moveSpeed * Time.deltaTime;
            }

            LookAtPlayer(currentFocusPlayer);
        }
    }
}
