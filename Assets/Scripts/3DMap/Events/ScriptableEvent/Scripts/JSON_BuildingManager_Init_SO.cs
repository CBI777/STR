using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingManagerInit", menuName = "ScriptableObjects/Event/JSON Init/Building Manager")]
public class JSON_BuildingManager_Init_SO : ScriptableObject
{
    /// <summary>
    /// Subscibe this event if you want to receive :
    /// Initialization values for building - BuildingManager
    /// </summary>
    [System.NonSerialized]
    public UnityEvent<List<BuildingInfo>> jsonBuildingInitEvent;

    private void OnEnable()
    {
        jsonBuildingInitEvent = new UnityEvent<List<BuildingInfo>>();
    }

    public void Broadcast_Building_Init(List<BuildingInfo> bi)
    {
        //Debug
        Debug.Log("Building Manager Init Event");
        jsonBuildingInitEvent.Invoke(bi);
    }
}

