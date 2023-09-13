using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterPanelOnOff", menuName = "ScriptableObjects/Event/3D_Map_UI/CharacterPanel OnOff")]
public class MapUI_CharacterPanel_OnOff_SO : ScriptableObject
{
    [System.NonSerialized]
    public UnityEvent<bool> characterPanelEvent;

    private void OnEnable()
    {
        characterPanelEvent = new UnityEvent<bool>();
    }

    public void Broadcast_characterPanel_Event(bool onOff)
    {
        Debug.Log("CharacterPanel " + onOff);
        characterPanelEvent.Invoke(onOff);
    }
}
