using UnityEngine;
using System.Collections.Generic;

public class RayDrawer
{
    private struct RayDrawData
    {
        public Vector3 origin;
        public Vector3 direction;

        public Color color;
        public float maxRayLength;

        public float maxTime;
        public float currentTime;

        public RayDrawData(Vector3 origin, Vector3 direction, Color color, float maxTime, float maxRayLength = 25f)
        {
            this.origin = origin;
            this.direction = direction;
            this.color = color;
            this.maxRayLength = maxRayLength;

            this.maxTime = maxTime;
            this.currentTime = 0;
        }

        public bool Draw()
        {
            if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, maxRayLength))
            {
                Debug.DrawLine(origin, hitInfo.point, color);
            }
            else
            {
                Debug.DrawRay(origin, direction * maxRayLength, color);
            }
            
            // Dispose of ray after the time passed, return true if it can be removed from array
            currentTime += Time.deltaTime;
            if (currentTime > maxTime)
            {
                return true;
            }

            return false;
        }
    }

    private static List<RayDrawData> rays = new List<RayDrawData>();

    public static void Update()
    {
        for(int i = 0; i < rays.Count; i++)
        {
            var ray = rays[i];
            bool isdead = ray.Draw();
            rays[i] = ray;
            if(isdead)
            {
                rays.RemoveAt(i--);
            }
        }
    }

    public static void DrawRay(Vector3 origin, Vector3 direction, Color color, float rayTime)
    {
        rays.Add(new RayDrawData(origin, direction, color, rayTime));
    }
}
    