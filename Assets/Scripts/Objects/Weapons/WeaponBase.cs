using JetBrains.Annotations;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public int Damage;

    public abstract void Use();

    public void ApplyDamage(PlayerHealth player)
    {
        player.ApplyDamage(Damage);
    }
}
