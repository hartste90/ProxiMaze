using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldData", menuName = "ProxiMaze/WorldData", order = 1)]
public class WorldData : ScriptableObject
{
    public List<TrialData> trialList;

    public List<TrialData> GetTrialDataList()
    {
        return trialList;
    }

}