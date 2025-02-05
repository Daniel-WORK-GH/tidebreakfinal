using UnityEngine;

public class MissionBase : MonoBehaviour
{
    public EnemyBase[] Enemies;

    [TextArea(3, 10)]
    public string MissionExplenation = "";

    public bool DrawGizmos = true;

    public bool IsFinished => isFinished;
    protected bool isFinished = false;

    protected MissionHandler MissionHandler;

    public void Start()
    {
        MissionHandler = Object.FindObjectsByType(typeof(MissionHandler), FindObjectsSortMode.None)[0] as MissionHandler;

        foreach(var e in Enemies)
        {
            e.SetData(e.transform.position, e.transform.rotation);
            e.Disable();
        }
    }

    public virtual void OnMissionStart()
    {
        foreach(var e in Enemies)
        {
            e.ResetAndEnable();
        }
    }

    public virtual void OnMissionAbandon()
    {
        foreach (EnemyBase enemy in Enemies)
        {
            enemy.Disable();
        }
    }

    public virtual void OnMissionComplete()
    {
        foreach (EnemyBase enemy in Enemies)
        {
            enemy.Disable();
            Destroy(enemy);
        }

        isFinished = true;
    }

    protected bool IsCurrentMission()
    {
        return this.gameObject.name == MissionHandler.currentMission.name;
    }
}
