using System.Collections.Generic;
using UnityEngine;

public class JsonLoader_3DMap : MonoBehaviour
{
    //this is going to be the name of json file for this stage (Ex.FT-1)
    [SerializeField]
    private string JsonFileName;

    //Scriptable Object event for initializing background
    [SerializeField]
    private JSON_GroundManager_Init_SO groundManager;
    //Scriptable Object event for initializing buildings
    [SerializeField]
    private JSON_BuildingManager_Init_SO buildingManager;
    //Scriptable Object event for initializing nodes
    [SerializeField]
    private JSON_NodeManager_Init_SO nodeManager;
    //Manager who's going to initialize various camera info
    [SerializeField]
    private JSON_CameraManager_Init_SO cameraManager;


    //temporary
    void Start()
    {
        var tempData = JsonUtility.FromJson<StageInfo>(
            Resources.Load<TextAsset>("MapJson/" + JsonFileName).ToString());

        groundManager.Broadcast_Background_Init(tempData.GroundInfo[0]);
        buildingManager.Broadcast_Building_Init(tempData.BuildingInfo);
        nodeManager.Broadcast_Node_Init(tempData.NodeInfo);
        cameraManager.Broadcast_Camera_Init(tempData.CameraConfigInfo[0]);
    }

    void SetJsonName(string name)
    {
        this.JsonFileName = name;
    }
}
