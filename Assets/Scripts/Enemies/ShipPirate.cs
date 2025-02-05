using UnityEngine;

public class ShipPirate : EnemyBase
{
    public Cannon[] cannons;

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        
        SelectPlayerFocus();

        if(currentFocusPlayer != null)
        {
            LookAtPlayer(currentFocusPlayer);

            foreach(var cannon in cannons)
            {
                cannon.Use();
            }
        }
    }
}
