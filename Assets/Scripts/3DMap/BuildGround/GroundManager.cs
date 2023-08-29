using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    //temporal [SerializeField] to debug and see the value
    //Bg Img name
    [SerializeField]
    private string bgName;
    //Rect that we need to crop the img in the size and area that we want
    [SerializeField]
    private Rect cropRect;
    //Pixel value for new sprite that we'll create by cropping the image in runtime.
    [SerializeField]
    private float pixPerUnit;
    //pivot for new sprite that we'll create by cropping the image in runtime.
    [SerializeField]
    private Vector2 pivot;

    //actual gameobject that has the sprite renderer for the background img.
    //this will be initailized in start, since the renderer will be attached to the same object as this script
    private SpriteRenderer bgImg;

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

    void Awake()
    {
        this.bgImg = this.GetComponent<SpriteRenderer>();
    }

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
        bgImg.GetComponent<SpriteRenderer>().sprite = Sprite.Create(Resources.Load<Texture2D>(Whereabouts.BackgroundTextures + name), rect, piv, pixel);
    }
}