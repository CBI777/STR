using UnityEngine;

public class UI_3DM_CP_PortraitController : UI_FoldNStretch
{
    //======Event Related======//
    [SerializeField]
    private MapUI_CharacterPanel_OnOff_SO chara_onoff;
    //=========================//

    private void OnEnable()
    {
        chara_onoff.characterPanelEvent.AddListener(FoldNStretchUI);
    }
    private void OnDisable()
    {
        chara_onoff.characterPanelEvent.RemoveListener(FoldNStretchUI);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject gObjects in uiHolders)
        {
            uiRects.Add(gObjects.GetComponent<RectTransform>());
            scaleShow.Add(Vector3.one);
            scaleHide.Add(new Vector3(1.0f, 0.0f, 1.0f));
            showTweenTime.Add(0.2f);
            hideTweenTime.Add(0.2f);
        }
    }
}
