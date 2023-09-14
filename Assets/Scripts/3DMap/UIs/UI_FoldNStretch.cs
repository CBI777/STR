using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class UI_FoldNStretch : MonoBehaviour
{
    /// <summary>
    /// List of gameobjects that you want to manipulate - they will either appear or disappear at the same time.
    /// </summary>
    [SerializeField]
    protected List<GameObject> uiHolders;

    /// <summary>
    /// List of recttransforms of uiHolders, to make life easier.
    /// </summary>
    [SerializeField]
    protected List<RectTransform> uiRects;

    /// <summary>
    /// List that stores final scale of each ui when it is shown, foreach uis inside uiHolders.
    /// </summary>
    [SerializeField]
    protected List<Vector3> scaleShow;

    /// <summary>
    /// List that stores final scale of each ui when it is disabled / disappeared, foreach uis inside uiHolders.
    /// </summary>
    [SerializeField]
    protected List<Vector3> scaleHide;

    /// <summary>
    /// List of floats that decides TweeningTime when you make ui appear.
    /// </summary>
    [SerializeField]
    protected List<float> showTweenTime;
    /// <summary>
    /// List of floats that decides TweeningTime when you make ui disappear.
    /// </summary>
    [SerializeField]
    protected List<float> hideTweenTime;

    /// <summary>
    /// Disables all UI holders stored inside uiHolders list.
    /// Will be used right after tweening inside FoldNStretchUI, if you did not touch anything.
    /// </summary>
    protected void DisablePortMask()
    {
        foreach(GameObject gObject in uiHolders)
        {
            gObject.SetActive(false);
        }
    }

    /// <summary>
    /// Tween the uiHolder so that it shows folding or streching ~ish effect. Acts differently depending on state.
    /// </summary>
    /// <param name="state">True: stretch all uiHolders, so that they become shown. False: fold all uiHolders, so that they can be disabled</param>
    protected void FoldNStretchUI(bool state)
    {
        int tempCount = uiHolders.Count;
        //Be mindful that we expect uiHolders to be disabled and folded.
        //make ui visible.
        if (state)
        {
            for(int i = 0; i < tempCount; i++)
            {
                uiHolders[i].SetActive(true);
                uiRects[i].DOScale(scaleShow[i], showTweenTime[i]);
            }
        }
        else
        {
            //make ui disappear
            for (int i = 0; i < tempCount; i++)
            {
                uiRects[i].DOScale(scaleHide[i], hideTweenTime[i]).onComplete = DisablePortMask;
            }
        }
    }
}
