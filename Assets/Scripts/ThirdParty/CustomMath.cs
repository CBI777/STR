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
}
