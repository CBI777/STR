using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeManagerInit", menuName = "ScriptableObjects/Event/JSON Init/Node Manager")]
public class JSON_NodeManager_Init_SO : ScriptableObject
{
    /// <summary>
    /// Subscibe this event if you want to receive :
    /// Initialization values for node - NodeManager
    /// </summary>
    [System.NonSerialized]
    public UnityEvent<List<NodeInfo>> jsonNodeInitEvent;

    private void OnEnable()
    {
        jsonNodeInitEvent = new UnityEvent<List<NodeInfo>>();
    }

    public void Broadcast_Node_Init(List<NodeInfo> ni)
    {
        //Debug
        Debug.Log("Node Manager Init Event");
        jsonNodeInitEvent.Invoke(ni);
    }
}

