using System;
using UnityEngine;

public static class NoiseSystem
{
    public static Action<Vector3> OnNoiseMade;

    public static void MakeNoise(Vector3 position)
    {
        OnNoiseMade?.Invoke(position);
    }
}
