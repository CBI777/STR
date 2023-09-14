using UnityEngine;
using DG.Tweening;

public class UI_3DM_CP_CommandController : MonoBehaviour
{
    //This value refers to the command (small)panel - which holds up hp bar and btns
    [SerializeField]
    private GameObject commandHolder;
    //RectTransform of the command panel, to make life easier.
    private RectTransform commandRect;

    //Since CommandPanel is tweening in and out by moving posX,
    //we need values for setting appropriate position according to the initial place of the ui
    private float initX;
    private float endX;

    //Debug!
    [SerializeField]
    private float tweenTime = 0.2f;

    //======Event Related======//
    [SerializeField]
    private MapUI_CharacterPanel_OnOff_SO chara_onoff;
    //=========================//
    private void OnEnable()
    {
        chara_onoff.characterPanelEvent.AddListener(ChangeCommand);
    }
    private void OnDisable()
    {
        chara_onoff.characterPanelEvent.RemoveListener(ChangeCommand);
    }

    private void Start()
    {
        //the reason why we calc endX first is because 
        this.commandRect = this.commandHolder.GetComponent<RectTransform>();
        this.endX = this.commandRect.anchoredPosition.x;
        this.initX = this.endX - this.commandRect.rect.width;
    }

    private void DisableCommandHolder()
    {
        this.commandHolder.SetActive(false);
    }

    /// <summary>
    /// Tween the Command holder in by moving posx,  according to input
    /// </summary>
    /// <param name="state">show panel if true, hide the panel if false</param>
    private void ChangeCommand(bool state)
    {
        if(state)
        {
            this.commandHolder.SetActive(true);
            commandRect.DOAnchorPosX(initX, tweenTime);
        }
        else
        {
            commandRect.DOAnchorPosX(endX, tweenTime).onComplete = DisableCommandHolder;
        }
    }
}
