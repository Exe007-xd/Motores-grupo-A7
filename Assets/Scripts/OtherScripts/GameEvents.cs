using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<int, int> onPlayerHealthChanged;

    public static void OnPlayerHealthChanged(int currentHealth, int maxHealth) 
    {
        onPlayerHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public static Action onRoomTriggered;
        public static void OnRoomTriggered() 
        {
            onRoomTriggered?.Invoke();
        }

    public static Action onStartCrowbarQTE;

    public static void OnStartCrowbarQTE()
    {
        onStartCrowbarQTE?.Invoke();
    }

    public static Action onCrowbarQTESuccess;
    public static void OnCrowbarQTESuccess()
    {
        onCrowbarQTESuccess?.Invoke();
    }

    public static Action onCrowbarQTEFailure;
    public static void OnCrowbarQTEFailure()
    {
        onCrowbarQTEFailure?.Invoke();
    }
}
