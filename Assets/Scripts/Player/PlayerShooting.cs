using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public WeaponBase[] weapons;

    private int CurrentWeapon;


    void Start()
    {
        CurrentWeapon = 0; // TODO : Change to start at -1 to indicate hands
    }

    void Update()
    {        
        if(Input.GetMouseButton(0))
        {
            Debug.Log("Pew!");
            weapons[CurrentWeapon].Use();
        }
    }
}
