
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;    

    public UiHider hider;
    
    public float healthTimer = 1000;
    private float elpTime = 0;

    void Update()
    {
        if(CurrentHealth <= 0)
        {
            hider.ForceShow();
            DoDelayAction(3);
        }

        elpTime += Time.deltaTime;

        if(elpTime > healthTimer)
        {
            CurrentHealth += 5;
            elpTime = 0;
        }

        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
    }

    void DoDelayAction(float delayTime)
    {
        StartCoroutine(RestartLevel(delayTime));
    }

    IEnumerator RestartLevel(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}