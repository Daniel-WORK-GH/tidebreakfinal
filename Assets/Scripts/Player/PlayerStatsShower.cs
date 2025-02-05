using TMPro;
using UnityEngine;

public class PlayerStatsShower : MonoBehaviour
{
    public TMP_Text textbox;

    private PlayerMover mover;
    private PlayerHealth health;

    void Start()
    {
        mover = GetComponent<PlayerMover>();
        health = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        textbox.text = $"Health : {health.CurrentHealth} / {health.MaxHealth}\nStamina : {(int)mover.reminingRunTime} / {mover.maxRunTime}";
    }
}
