using UnityEngine;
using DG.Tweening;

public class UI_3DM_CP_PortraitController : MonoBehaviour
{
    //This value holds the portrait mask. Will use this to disable OR, in other words, make portrait appear and disappear
    [SerializeField]
    private GameObject portMask;
    //This value holds the outer frame. 
    [SerializeField]
    private GameObject frame;

    //Each Refers to portrait and frame's rectTransform
    private RectTransform portRect;
    private RectTransform frameRect;

    //Debug!
    [SerializeField]
    private float tweenTime = 0.2f;

    //======Event Related======//
    [SerializeField]
    private MapUI_CharacterPanel_OnOff_SO chara_onoff;
    //=========================//

    private void OnEnable()
    {
        chara_onoff.characterPanelEvent.AddListener(ChangePortrait);
    }
    private void OnDisable()
    {
        chara_onoff.characterPanelEvent.RemoveListener(ChangePortrait);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.portRect = portMask.GetComponent<RectTransform>();
        this.frameRect = frame.GetComponent<RectTransform>();
    }

    private void DisablePortMask()
    {
        this.portMask.SetActive(false);
    }
    private void DisableFrame()
    {
        this.frame.SetActive(false);
    }
    /// <summary>
    /// Tween the portrait by y, according to input
    /// </summary>
    /// <param name="state">show panel if true, hide the panel if false</param>
    private void ChangePortrait(bool state)
    {
        if (state)
        {
            this.portMask.SetActive(true);
            this.frame.SetActive(true);
            portRect.DOScaleY(1.0f, tweenTime);
            frameRect.DOScaleY(1.0f, tweenTime);
        }
        else
        {
            portRect.DOScaleY(0.0f, tweenTime).onComplete = DisablePortMask;
            frameRect.DOScaleY(0.0f, tweenTime).onComplete = DisableFrame;
        }
    }
}
