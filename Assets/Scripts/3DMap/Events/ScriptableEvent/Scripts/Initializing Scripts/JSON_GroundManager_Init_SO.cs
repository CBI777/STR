using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "GroundManagerInit", menuName = "ScriptableObjects/Event/JSON Init/Background Manager")]
public class JSON_GroundManager_Init_SO : ScriptableObject
{
    /// <summary>
    /// Subscibe this event if you want to receive :
    /// Initialization values for background - GroundManager
    /// </summary>
    [System.NonSerialized]
    public UnityEvent<GroundInfo> jsonGroundInitEvent;

    private void OnEnable()
    {
        jsonGroundInitEvent = new UnityEvent<GroundInfo>();
    }

    public void Broadcast_Background_Init(GroundInfo gi)
    {
        //Debug
        Debug.Log("Ground Manager Init Event");
        jsonGroundInitEvent.Invoke(gi);
    }
}
