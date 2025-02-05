using UnityEngine;

public class AliminationMission : MissionBase
{
    private int activeEnemies = 0;

    private string baseQuestExplenation;

    public override void OnMissionStart()
    {
        base.OnMissionStart();

        baseQuestExplenation = base.MissionExplenation;
    }

    void Update()
    {
        if(base.isFinished || !IsCurrentMission()) return;
        
        activeEnemies = 0;
        foreach(var enemy in Enemies)
        {
            if(enemy.health > 0)
            {
                activeEnemies++;
            }
        }

        if(activeEnemies == 0)
        {
            MissionHandler.CompleteCurrentMission();
        }

        base.MissionExplenation = baseQuestExplenation  + $"\n{activeEnemies} / {Enemies.Length}.";
    }
}
