using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//XX : ����ó����, initialization�� ���ؼ� ����Ѵ�.
//�߸�, ��Ʈ�е帮�� ���̴Ͽ�, ������Ÿ �������� �ι� ��ȭ��, 
public enum ConqForce
{
    NA, SD, RC
};

//�Ʊ�����, �ƹ��͵� ����, ���� �Ұ�
public enum NodeSpec
{
    FB, NR, PH
}

//���� ����, 
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
