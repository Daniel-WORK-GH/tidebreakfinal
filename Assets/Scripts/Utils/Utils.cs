using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Utils : MonoBehaviour 
{
    public void Update()
    {
        RayDrawer.Update();
    }

    public static void DrawRay(Vector3 origin, Vector3 direction, Color color, float rayTime)
    {
        RayDrawer.DrawRay(origin, direction, color, rayTime);
    }
    
    public static T[] GetListOfType<T>() where T : Object
    {
        Object[] objs = Object.FindObjectsByType(typeof(T), FindObjectsSortMode.None);
        T[] ts = new T[objs.Length];

        for(int i = 0; i < objs.Length; i++)
        {
            ts[i] = objs[i] as T;
        }

        return ts;
    }

    public static PlayerMover[] GetPlayers()
    {
        return GetListOfType<PlayerMover>();
    }

    public static T GetClosest<T>(Vector3 position, T[] ts) where T : Object
    {
        T result = null;
        float minDist = float.MaxValue;
        
        foreach (T t in ts)
        {
            float dist = Vector3.Distance((t as Object).GameObject().transform.position, position);
            if (dist < minDist)
            {
                result = t;
                minDist = dist;
            }
        }

        return result;
    }

}