using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected PlayerMover[] players = new PlayerMover[0];
    protected PlayerMover currentFocusPlayer;

    public int maxHealth;
    public int health;

    [Tooltip("Whne the enemy locks in on the player")]
    public float lockRange;
    [Tooltip("Whne the enemy stop following the player")]
    public float releaseRange;
    public float moveSpeed = 5f;
    public bool lookAtPlayer = true;

    public bool drawGizmos = false;

    private Vector3 startPosition;
    private Quaternion startRotation;

    protected void SelectPlayerFocus()
    {
        players = Utils.GetPlayers();

        PlayerMover temp = Utils.GetClosest(transform.position, players);

        if(temp != null && Vector3.Distance(temp.transform.position, transform.position) < lockRange)
        {
            currentFocusPlayer = temp;
        }
        else if(currentFocusPlayer != null && Vector3.Distance(temp.transform.position, transform.position) < releaseRange)
        {
            // Keep focus
        }
        else 
        {
            currentFocusPlayer = null;
        }
    }

    protected void LookAtPlayer(PlayerMover player)
    {
        Vector3 direction = (currentFocusPlayer.transform.position - transform.position).normalized;
        
        if (lookAtPlayer)
        {
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
        }
    }

    public void SetData(Vector3 position, Quaternion rotation)
    {
        startPosition = position;
        startRotation = rotation;
    }

    public void ResetAndEnable()
    {
        health = maxHealth;
        transform.position = startPosition;
        transform.rotation = startRotation;
        gameObject.SetActive(true);
    }

    public void Disable() 
    {
        gameObject.SetActive(false);
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
    }

    public void Update()
    {
        if(health <= 0)
        {
            Disable();
        }
    }

    public void OnDrawGizmos()
    {
        if(drawGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lockRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, releaseRange);
        }
    }
}
