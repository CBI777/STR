using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeClickedScriptableObject", menuName = "ScriptableObjects/Event/Node/Node Clicked")]
public class NodeClicked_SO : ScriptableObject
{
    /// <summary>
    /// Subscibe this event if you want to receive :
    /// Index value of the node that has been clicked.
    /// </summary>
    [System.NonSerialized]
    public UnityEvent<int> nodeClickedEvent;

    private void OnEnable()
    {
        nodeClickedEvent = new UnityEvent<int>();
    }

    public void Broadcast_NodeClicked(int index)
    {
        //Debug
        Debug.Log("Node click broadcasted : " + index);
        nodeClickedEvent.Invoke(index);
    }
}
