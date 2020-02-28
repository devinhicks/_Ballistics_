using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringSolution
{
    public Nullable<Vector3> Calculate(Vector3 start, Vector3 end, float MuzzleV, Vector3 gravity)
    {
        Nullable<float> ttt = GetTimeToTarget(start, end, MuzzleV, gravity);
        if(!ttt.HasValue)
        {
            return null;
        }

        // where are we going!
        Vector3 delta = end - start;

        Vector3 n1 = delta * 2;
        Vector3 n2 = gravity * (ttt.Value * ttt.Value);
        float d = 2 * MuzzleV * ttt.Value;
        Vector3 solution = (n1 - n2) / d;

        return solution;
    }

    public Nullable<float> GetTimeToTarget(Vector3 start, Vector3 end, float MuzzleV, Vector3 gravity)
    {
        float ttt; // time to target

        // calculate vector from target back to start
        Vector3 delta = start - end;

        // calculate real-valued a,b,c coefficients of
        // a convential quadratic equation
        float a = gravity.sqrMagnitude;
        float b = -4 * (Vector3.Dot(gravity, delta) + MuzzleV * MuzzleV);
        float c = 4 * delta.sqrMagnitude;

        // check for no real solutions
        float b2minus4ac = (b * b) - (4 * a * c);
        if (b2minus4ac < 0)
        {
            return null;
        }

        // Find candidate times
        float time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b2minus4ac)) / (2 * a));
        float time1 = Mathf.Sqrt((-b - Mathf.Sqrt(b2minus4ac)) / (2 * a));

        // Find time to target
        if (time0 < 0)
        {
            if (time1 < 0)
            {
                // no valid times
                return null;
            }
            else
            {
                ttt = time1;
            }
        }

        else
        {
            if (time1 < 0)
            {
                ttt = time0;
            }
            else
            {
                ttt = Mathf.Min(time0, time1);
            }
        }

        return ttt;
    }
}
