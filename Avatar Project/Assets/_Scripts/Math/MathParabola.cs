using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MathParabola
{
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t, int inverted)
    {
        Func<float, float> f = x => -4 * height * x * x * inverted + 4 * height * x * inverted;

        Vector3 mid = Vector3.Lerp(start, end, t);

        return new Vector3(f(t) + Mathf.Lerp(start.x, end.x, t), mid.y, f(t) + Mathf.Lerp(start.z, end.z, t));
    }
}
