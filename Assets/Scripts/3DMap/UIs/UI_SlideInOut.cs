using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class UI_SlideInOut : MonoBehaviour
{
    /// <summary>
    /// List of gameobjects that you want to manipulate - they will slide in and out
    /// </summary>
    [SerializeField]
    protected List<GameObject> uiHolders;
    /// <summary>
    /// List of recttransforms of uiHolders, to make life easier.
    /// </summary>
    [SerializeField]
    protected List<RectTransform> uiRects;

    /// <summary>
    /// List of positions of UIs when they are shown.
    /// When UI is shown, this will be its position.
    /// </summary>
    [SerializeField]
    protected List<Vector2> showPos;

    /// <summary>
    /// List of positions of UIs when they are hidden.
    /// This position will be the destination of 'disappearing tweening'.
    /// After reaching this postion, uis will be disabled
    /// </summary>
    [SerializeField]
    protected List<Vector2> hidePos;

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
    /// Disables UI holder. Will be used right after tweening inside SlideInOutUI, if you did not touch anything.
    /// </summary>
    protected void DisableUiHolder()
    {
        foreach (GameObject gObject in uiHolders)
        {
            gObject.SetActive(false);
        }
    }

    /// <summary>
    /// Tween the uiHolder in certain direction. Acts differently depending on state.
    /// </summary>
    /// <param name="state">True: slides in uiHolder, so that it becomes shown. False: slides out uiHolder, so that it can be disabled</param>
    protected void SlideInOutUI(bool state)
    {
        int tempCount = uiHolders.Count;
        //Be mindful that we expect uiHolder to be hidden or disabled at initial state.
        //make ui visible.
        if (state)
        {
            for (int i = 0; i < tempCount; i++)
            {
                uiHolders[i].SetActive(true);
                uiRects[i].DOAnchorPos(showPos[i], showTweenTime[i]);
            }
        }
        else
        {
            //make ui disappear
            for (int i = 0; i < tempCount; i++)
            {
                uiRects[i].DOAnchorPos(hidePos[i], hideTweenTime[i]).onComplete = DisableUiHolder;
            }

        }
    }
}
