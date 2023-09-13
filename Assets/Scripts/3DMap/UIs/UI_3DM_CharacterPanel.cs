using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_3DM_CharacterPanel : MonoBehaviour
{
    [SerializeField]
    private bool charaActive = false;

    [SerializeField]
    private MapUI_CharacterPanel_OnOff_SO characterPanel;

    //TODO : This must be fixed!!!!!!!!!!!!!
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            charaActive = !charaActive;
            characterPanel.Broadcast_characterPanel_Event(charaActive);
        }
    }
}
