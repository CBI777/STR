using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundManager : MonoBehaviour, IPointerClickHandler
{
    //Bg Img name
    private string bgName;
    //Rect that we need to crop the img in the size and area that we want
    private Rect cropRect;
    //Pixel value for new sprite that we'll create by cropping the image in runtime.
    private float pixPerUnit;
    //pivot for new sprite that we'll create by cropping the image in runtime.
    private Vector2 pivot;
    //actual gameobject that has the sprite renderer for the background img.
    //this will be initailized in start, since the renderer will be attached to the same object as this script
    private SpriteRenderer bgImg;

    //collider that will be used to get 'click on floor' - undo click on sth.
    private BoxCollider2D boxcollider;

    //======Event Related======//
    [SerializeField]
    private JSON_GroundManager_Init_SO backgroundInitEvent;
    [SerializeField]
    private NodeClicked_SO nodeClicked_SO;
    //=========================//
    private void OnEnable()
    {
        backgroundInitEvent.jsonGroundInitEvent.AddListener(GetInitInfo);
    }
    private void OnDisable()
    {
        backgroundInitEvent.jsonGroundInitEvent.RemoveListener(GetInitInfo);
    }
    void Awake()
    {
        this.bgImg = this.GetComponent<SpriteRenderer>();
        this.boxcollider = this.GetComponent<BoxCollider2D>();
    }

    //=====================Event related Functions=======================//

    public void OnPointerClick(PointerEventData eventData)
    {
        BroadcastClickedNode();
    }

    private void BroadcastClickedNode()
    {
        nodeClicked_SO.Broadcast_NodeClicked(-1);
    }

    public void GetInitInfo(GroundInfo gd)
    {
        //Since manager IS the one that has the renderer, its transform is the size of plane
        this.transform.localScale = new Vector3(gd.planeScale[0], gd.planeScale[1], 1f);

        this.bgName = gd.bgName;
        this.pivot = new Vector2(gd.pivot[0], gd.pivot[1]);
        this.pixPerUnit = gd.pixPerUnit;
        //setting the rect to crop the image into sprite
        SetRect(gd.rectMinMax[0], gd.rectMinMax[1], gd.rectMinMax[2], gd.rectMinMax[3]);

        SetSprite(this.bgName, this.cropRect, this.pivot, this.pixPerUnit);
    }

    //===================================================================//


    private void SetRect(float minX, float maxX, float minY, float maxY)
    {
        this.cropRect.center = new Vector2(0f, 0f);
        this.cropRect.xMin = minX;
        this.cropRect.yMin = minY;
        this.cropRect.xMax = maxX;
        this.cropRect.yMax = maxY;
    }

    private void SetSprite(string name, Rect rect, Vector2 piv, float pixel)
    {
        bgImg.GetComponent<SpriteRenderer>().sprite = 
            Sprite.Create(Resources.Load<Texture2D>(Whereabouts.BackgroundTextures + name), rect, piv, pixel);
        this.boxcollider.size = this.bgImg.size;
    }

}