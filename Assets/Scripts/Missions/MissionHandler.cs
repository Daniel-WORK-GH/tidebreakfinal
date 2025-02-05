using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevelPhysics;
using UnityEngine.SceneManagement;

public class MissionHandler : MonoBehaviour
{
    public MissionBase[] missions;
    public MissionBase currentMission;
    public int currentMissionIndex;

    public UiHider canvasHider;
    public TMP_Text missionExplenation;

    public GameObject targetX;  

    public UiHider winHider;

    void Start()
    {
        StartNextMission();
    }

    public MissionBase GetCurrentMisison() 
    {
        return missions[currentMissionIndex];
    }

    public bool StartNextMission()
    {
        currentMissionIndex++;

        if(currentMissionIndex >= missions.Length)
        {
            winHider.ForceShow();
            DoDelayAction(3);
        }
        else
        {
            currentMission = missions[currentMissionIndex];
            currentMission.OnMissionStart();     
            canvasHider.ForceShow();

            targetX.transform.position = new Vector3(currentMission.transform.position.x, targetX.transform.position.y, currentMission.transform.position.z);
        }
        return true;
    }

    void Update()
    {
        missionExplenation.text = currentMission.MissionExplenation;
    }

    public void AbandonCurrentMission()
    {
        currentMission.OnMissionAbandon();
        currentMission = null;
    }

    public void CompleteCurrentMission()
    {
        currentMission.OnMissionComplete();
        currentMission = null;
        StartNextMission();
    }

    void DoDelayAction(float delayTime)
    {
        StartCoroutine(RestartLevel(delayTime));
    }

    IEnumerator RestartLevel(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
