using UnityEngine;

public class ShipCollisionFollower : MonoBehaviour
{
    public GameObject shipCollider;
    
    void Start()
    {
        
    }

    void Update()
    {
        shipCollider.transform.position = this.transform.position;
        shipCollider.transform.rotation = this.transform.rotation;
    }
}
