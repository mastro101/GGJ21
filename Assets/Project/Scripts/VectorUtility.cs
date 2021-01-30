using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtility
{
    public static Vector3 RandomV3OnPlaneY(float radiur)
    {
        float x, z;
        x = Random.Range(-radiur, radiur);
        z = Random.Range(-radiur, radiur);

        return new Vector3(x, 0f, z);
    }
}