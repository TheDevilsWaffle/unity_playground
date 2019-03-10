using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct IntVector2
{
    public int x;
    public int z;

    public IntVector2(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public static IntVector2 operator + (IntVector2 _a, IntVector2 _b)
    {
        _a.x += _b.x;
        _a.z += _b.z;
        return _a;
    }
}
