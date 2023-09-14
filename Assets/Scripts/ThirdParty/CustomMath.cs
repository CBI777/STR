using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomMath
{
    /// <summary>
    /// Returns Vector3 with clamped x and y value
    /// </summary>
    /// <param name="orig">The original vector that you want to clamp</param>
    /// <param name="xMin">x min value</param>
    /// <param name="xMax">x max value</param>
    /// <param name="yMin">y min value</param>
    /// <param name="yMax">y max value</param>
    /// <returns></returns>
    public static Vector3 ClampVector(Vector3 orig, float xMin, float xMax, float yMin, float yMax)
    {
        return new Vector3(Mathf.Clamp(orig.x, xMin, xMax), Mathf.Clamp(orig.y, yMin, yMax), orig.z);
    }
    /// <summary>
    /// Returns Vector3 with clameped x and y value, using rect
    /// </summary>
    /// <param name="orig">The original vector that you want to clamp</param>
    /// <param name="limit">Rect that represents the limit</param>
    /// <returns></returns>
    public static Vector3 ClampVector(Vector3 orig, Rect limit)
    {
        return new Vector3(Mathf.Clamp(orig.x, limit.xMin, limit.xMax),
            Mathf.Clamp(orig.y, limit.yMin, limit.yMax), orig.z);
    }

    /// <summary>
    /// Returns Vector3 with clamped x, y and z value
    /// </summary>
    /// <param name="orig">The original vector that you want to clamp</param>
    /// <param name="xMin">x min value</param>
    /// <param name="xMax">x max value</param>
    /// <param name="yMin">y min value</param>
    /// <param name="yMax">y max value</param>
    /// <param name="zMin">z min value</param>
    /// <param name="zMax">z max value</param>
    /// <returns></returns>
    public static Vector3 ClampVector(Vector3 orig, float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
    {
        return new Vector3(Mathf.Clamp(orig.x, xMin, xMax), Mathf.Clamp(orig.y, yMin, yMax), Mathf.Clamp(orig.z, zMin, zMax));
    }

    /// <summary>
    /// Returns Width and Height of the view frustum that's (distFromCam) from the camera, based on 16:9 ratio
    /// </summary>
    /// <param name="distFromCam">distance from the camera to the view frustum</param>
    /// <param name="fov">fov of the camera</param>
    /// <returns>X is width, Y is height</returns>
    public static Vector2 CameraViewWH(float distFromCam, float fov)
    {
        Vector2 answer;
        answer.y = Mathf.Abs(2.0f * distFromCam * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad));
        answer.x = answer.y * 16 / 9;

        return answer;
    }
    /// <summary>
    /// Returns Width and Height of the view frustum that's (distFromCam) from the camera, with a given ratio
    /// </summary>
    /// <param name="distFromCam">distance from the camera to the view frustum</param>
    /// <param name="fov">fov of the camera</param>
    /// <param name="ratio">ratio of (width/height)</param>
    /// <returns></returns>
    public static Vector2 CameraViewWH(float distFromCam, float fov, float ratio)
    {
        Vector2 answer;
        answer.y = Mathf.Abs(2.0f * distFromCam * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad));
        answer.x = answer.y * ratio;

        return answer;
    }

    /// <summary>
    /// Returns maxZoom, where width of viewfrustum matches maxWidth.
    /// </summary>
    /// <param name="maxWidth">maximum width that zoom can reach</param>
    /// <param name="fov">fov of camera</param>
    /// <returns></returns>
    public static float GetCameraViewDistance(float maxWidth, float fov)
    {
        float answer;
        answer = maxWidth*9 / 16;
        answer /= (2.0f * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad));

        return answer;
    }
    /// <summary>
    /// Returns maxZoom, where width of viewfrustum matches maxWidth.
    /// </summary>
    /// <param name="maxWidth">maximum width that zoom can reach</param>
    /// <param name="fov">fov of camera</param>
    /// <param name="ratio">ratio of (width/height)</param>
    /// <returns></returns>
    public static float GetCameraViewDistance(float maxWidth, float fov, float ratio)
    {
        float answer;
        answer = maxWidth / ratio;
        answer /= (2.0f * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad));

        return answer;
    }
}
