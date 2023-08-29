using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    //this is going to be the name of json file for this stage (Ex.FT-1)
    [SerializeField]
    private string JsonFileName;

    //Manager who's going to initialize building & backGround
    [SerializeField]
    private GroundManager groundManager;
    [SerializeField]
    private BuildingManager buildingManager;
    //Manager who's going to initialize nodes
    [SerializeField]
    private NodeManager nodeManager;
    //Manager who's going to initialize various camera info
    [SerializeField]
    private CameraControl cameraManager;


    //temporary
    void Start()
    {
        var tempData = JsonUtility.FromJson<StageInfo>(
            Resources.Load<TextAsset>("MapJson/" + JsonFileName).ToString());

        this.groundManager.GetInitInfo(tempData.GroundInfo[0]);
        this.buildingManager.GetInitInfo(tempData.BuildingInfo);
        this.nodeManager.GetInitInfo(tempData.NodeInfo);
    }

    void SetJsonName(string name)
    {
        this.JsonFileName = name;
    }
}
