using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//XX : 조건처리용, initialization을 위해서 사용한다.
//중립, 스트론드리움 도미니온, 코프리타 민주주의 인민 공화국, 
public enum ConqForce
{
    NA, SD, RC
};

//아군기지, 아무것도 없음, 진입 불가
public enum NodeSpec
{
    FB, NR, PH
}

//문제 없음, 
public enum NodeState
{
    nor
}

[System.Serializable]
public class NodeInfo
{
    public int index;
    public string conq;
    public string spec;
    public List<string> state;
    public List<float> xyz;
    public List<int> conn;
}

[System.Serializable]
public class BuildingInfo
{
    public int index;
    public string prefabName;
    public List<float> xyz;
    public List<float> rotation;
    public List<float> scale;
}

[System.Serializable]
public class GroundInfo
{
    public List<float> planeScale;
    public string bgName;
    public List<float> rectMinMax;
    public float pixPerUnit;
    public List<float> pivot;
}



[System.Serializable]
public class CameraConfigInfo
{
    public List<float> initCamPos;
    public List<float> camLimit;
    public List<float> camZoomLimit;
}

[System.Serializable]
public class StageInfo
{
    public List<NodeInfo> NodeInfo;
    public List<BuildingInfo> BuildingInfo;
    public List<GroundInfo> GroundInfo;
    public List<CameraConfigInfo> CameraConfigInfo;
}
