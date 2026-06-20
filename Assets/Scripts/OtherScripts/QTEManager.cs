using UnityEngine;
using UnityEngine.InputSystem;

public class QTEManager : MonoBehaviour
{
    public int pressesNeeded = 8;
    public float totalTime = 8f;

    private int currentPresses;
    private float timer;
    private bool running;

    void Update()
    {
        if (!running)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            FailQTE();
        }
    }

    public void StartQTE()
    {
        running = true;
        timer = totalTime;
        currentPresses = 0;

        Debug.Log("QTE Started");
    }

    public void OnQTEPressed(InputAction.CallbackContext context)
    {
        if (!running)
            return;

        if (!context.performed)
            return;

        currentPresses++;

        Debug.Log($"{currentPresses}/{pressesNeeded}");

        if (currentPresses >= pressesNeeded)
        {
            SuccessQTE();
        }
    }

   
    void SuccessQTE()
    {
        GameEvents.OnCrowbarQTESuccess();
    }

    void FailQTE()
    {
      GameEvents.OnCrowbarQTEFailure();

    }
}
