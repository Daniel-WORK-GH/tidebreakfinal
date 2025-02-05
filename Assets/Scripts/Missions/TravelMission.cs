using UnityEngine;

public class TravelMission : MissionBase
{
    public Vector3 ArriveLocation;
    public float LocationRadius;

    void Update()
    {
        if(base.isFinished || !IsCurrentMission()) return;

        PlayerMover player = Utils.GetPlayers()[0];

        if(Vector3.Distance(player.transform.position, ArriveLocation) <= LocationRadius)
        {
            MissionHandler.CompleteCurrentMission();
        }
    }

    void OnDrawGizmos()
    {
        if(DrawGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(ArriveLocation, LocationRadius);
        }
    }
}
