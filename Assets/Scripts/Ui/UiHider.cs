using UnityEngine;
using UnityEngine.InputSystem;

public class UiHider : MonoBehaviour
{
    public GameObject[] objs;
    public KeyCode OpenKey = KeyCode.O;

    public bool Shown;
    public float maxShowTime;
    private float elpTime;

    void Start()
    {
        if(Shown)
        {
            elpTime = 0;
        }
        else 
        {
            elpTime = maxShowTime;
        }

        foreach(GameObject obj in objs)
        {
            obj.SetActive(Shown);
        }
    }

    void Update()
    {
        if(OpenKey == KeyCode.None) return;

        if(Input.GetKey(OpenKey))
        {
            elpTime = 0;
            Shown = true;
        }

        if(elpTime > maxShowTime)
        {
            Shown = false;
        }

        foreach(GameObject obj in objs)
        {
            obj.SetActive(Shown);
        }

        elpTime += Time.deltaTime * 1000;
    }

    public void ForceShow()
    {
        elpTime = 0;
        Shown = true;
        foreach(GameObject obj in objs)
        {
            obj.SetActive(Shown);
        }
    }

    public void ForceHide()
    {
        elpTime = maxShowTime;
        Shown = false;
        foreach(GameObject obj in objs)
        {
            obj.SetActive(Shown);
        }
    }
}
