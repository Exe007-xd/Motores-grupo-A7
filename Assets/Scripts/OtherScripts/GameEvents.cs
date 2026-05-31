using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<int, int> onPlayerHealthChanged;

    public static void OnPlayerHealthChanged(int currentHealth, int maxHealth) 
    {
        onPlayerHealthChanged?.Invoke(currentHealth, maxHealth);
    }

  
}
