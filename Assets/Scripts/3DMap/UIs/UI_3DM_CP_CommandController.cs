using UnityEngine;

public class UI_3DM_CP_CommandController : UI_SlideInOut
{
    //======Event Related======//
    [SerializeField]
    private MapUI_CharacterPanel_OnOff_SO chara_onoff;
    //=========================//
    private void OnEnable()
    {
        chara_onoff.characterPanelEvent.AddListener(SlideInOutUI);
    }
    private void OnDisable()
    {
        chara_onoff.characterPanelEvent.RemoveListener(SlideInOutUI);
    }

    private void Start()
    {
        for(int i = 0; i<uiHolders.Count; i++)
        {
            uiRects.Add(uiHolders[i].GetComponent<RectTransform>());
            //This will make UI appear by sliding from right to left
            this.hidePos.Add(this.uiRects[i].anchoredPosition);
            //And make UI disappear by sliding from left to right
            this.showPos.Add(this.hidePos[i] - new Vector2(this.uiRects[i].rect.width + 20f, 0f));
            this.showTweenTime.Add(0.1f + (0.05f * i));
            this.hideTweenTime.Add(0.2f - (0.05f * i));
        }
    }
}
