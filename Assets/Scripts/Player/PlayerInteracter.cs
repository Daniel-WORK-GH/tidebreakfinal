using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteracter : MonoBehaviour
{
    public Camera Camera;

    public GameObject map;
    public Canvas minimap;

    public UiHider[] uiHiders;

    public GameObject Ladder;
    public Transform ladderTpPos;
    public GameObject player;

    private PlayerMover mover;
    private PlayerFirstPersonCam cammover;
    private PlayerShooting playershooting;

    private bool InMenu = false;

    void Start()
    {
        mover = GetComponent<PlayerMover>();
        cammover = GetComponent<PlayerFirstPersonCam>();
        playershooting = GetComponent<PlayerShooting>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray (Camera.transform.position, Camera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast (ray, out hit, 3))
            {
                if(map.name == hit.collider.gameObject.name)
                {
                    InMenu = true;
                    minimap.gameObject.SetActive(true);
                }
                if(Ladder.name == hit.collider.gameObject.name)
                {
                    player.transform.position = ladderTpPos.position;
                }
            }
        }     

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            InMenu = false; 
            minimap.gameObject.SetActive(false);
        }

        mover.enabled = !InMenu;
        cammover.enabled = !InMenu;
        playershooting.enabled = !InMenu;

        foreach(var hider in uiHiders)
        {
            if(!InMenu)
            {
                hider.ForceHide();
                hider.gameObject.SetActive(!InMenu);
            }
        }
    }
}
