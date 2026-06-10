using System;
using UnityEngine;

public static class NoiseSystem
{
    public static Action<Vector3, float> OnNoiseMade;

    public static void MakeNoise(
        Vector3 position,
        float radius)
    {
        OnNoiseMade?.Invoke(position, radius);
    }
}