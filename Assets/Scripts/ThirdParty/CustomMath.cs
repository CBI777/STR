using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomMath
{
    /// <summary>
    /// Returns value if it is in between min and max. Returns min or max if value is smaller or bigger than min or max.
    /// </summary>
    /// <param name="value">the value that you want to compare with min and max value</param>
    /// <param name="min">minimum limit of the value</param>
    /// <param name="max">maximum limit of the value</param>
    /// <returns></returns>
    public static float clamp(float value, float min, float max)
    {
        //This function is used returns value if it is in between min and max.
        //However, it returns min or max if it is smaller than min or bigger than max.
        return (value < min) ? min : (value > max) ? max : value;
    }

    /// <summary>
    /// Returns height(pivot = 0.5f) of the camera's near plane
    /// </summary>
    /// <param name="cam">camera that you want to calc the plane</param>
    /// <returns></returns>
    public static float GetCamHeight(Camera cam)
    {
        float a = cam.nearClipPlane; //get length
        float A = cam.fieldOfView * 0.5f; //get angle
        A = A * Mathf.Deg2Rad; // convert for radian

        return (Mathf.Tan(A) * a);
    }


    /// <summary>
    /// Returns current cam's near plane dimension in form of Rect
    /// </summary>
    /// <param name="cam">camera that you want to calc the plane</param>
    /// <returns>Rect of near plane</returns>
    public static Rect NearPlaneDimensions(Camera cam)
    {
        Rect temp = new Rect();

        float h = GetCamHeight(cam);
        float w = (h / cam.pixelHeight) * cam.pixelWidth;

        temp.xMin = -w;
        temp.xMax = w;
        temp.yMin = -h;
        temp.yMax = h;

        return temp;
    }
}
