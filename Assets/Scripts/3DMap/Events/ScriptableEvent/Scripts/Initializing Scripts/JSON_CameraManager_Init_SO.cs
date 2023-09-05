using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraManagerInit", menuName = "ScriptableObjects/Event/JSON Init/Camera Manager")]
public class JSON_CameraManager_Init_SO : ScriptableObject
{
    /// <summary>
    /// Subscibe this event if you want to receive :
    /// Initialization values for Camera Config - CameraManager
    /// </summary>
    [System.NonSerialized]
    public UnityEvent<CameraConfigInfo> jsonCameraInitEvent;

    private void OnEnable()
    {
        jsonCameraInitEvent = new UnityEvent<CameraConfigInfo>();
    }

    public void Broadcast_Camera_Init(CameraConfigInfo ci)
    {
        //Debug
        Debug.Log("Camera Manager Init Event");
        jsonCameraInitEvent.Invoke(ci);
    }
}