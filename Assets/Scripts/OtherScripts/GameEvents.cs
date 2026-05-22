using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<int, int> onPlayerDamageHealth;

    public static void OnPlayerDamageHealth(int currentHealth, int maxHealth) 
    {
        onPlayerDamageHealth?.Invoke(currentHealth, maxHealth);
    }
}
