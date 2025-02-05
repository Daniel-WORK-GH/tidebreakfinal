using UnityEngine;

public class PlayerShipHandler : MonoBehaviour
{
    public bool inShip = false;
    public ShipMover ship;

    private Rigidbody rb;
    private new CapsuleCollider collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(inShip)
            {
                bool result = ship.TryLeaveShip(this);

                if(result)
                {
                    ship = null;
                    inShip = false;
                    rb.isKinematic = false;  
                    collider.enabled = true;
                    transform.SetParent(null);    
                }
            }
            else
            {
                ShipMover closestShip = Utils.GetClosest(transform.position, Utils.GetListOfType<ShipMover>());

                bool result = closestShip.TryEnterShip(this);

                if(result)
                {
                    ship = closestShip;
                    inShip = true;          
                    rb.isKinematic = true;
                    collider.enabled = false;
                    transform.SetParent(ship.transform);
                }
            }
        }
    }
}
