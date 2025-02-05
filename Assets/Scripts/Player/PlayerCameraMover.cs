using UnityEngine;

public class PlayerCameraMover : MonoBehaviour
{
    public Camera mainCamera; // The camera rendering the scene.
    public Transform shipCameraPosition;

    private Vector3 originalCamPos;
    private Vector3 shipCamPos;

    PlayerShipHandler psh;

    void Start()
    {
        psh = GetComponent<PlayerShipHandler>();

        originalCamPos = mainCamera.transform.position - transform.position;
        shipCamPos = shipCameraPosition.position - transform.position;
    }

    void Update()
    {
        if(psh.inShip)
        {
            mainCamera.transform.position = shipCamPos + transform.position;
        }
        else
        {
            mainCamera.transform.position = originalCamPos + transform.position;
        }
    }
}
