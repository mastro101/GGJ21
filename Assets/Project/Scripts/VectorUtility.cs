﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtility
{
    public static Vector3 RandomV3OnPlaneY(float radius)
    {
        float x, z;
        x = Random.Range(-radius, radius);
        z = Random.Range(-radius, radius);

        return new Vector3(x, 0f, z);
    }
    
    public static Vector3 RandomV3(float radius)
    {
        float x, y, z;
        x = Random.Range(-radius, radius);
        z = Random.Range(-radius, radius);
        y = Random.Range(-radius, radius);

        return new Vector3(x, y, z);
    }
}