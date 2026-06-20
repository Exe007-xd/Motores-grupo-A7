using UnityEngine;

public class TriggerRoom : MonoBehaviour
{
   private bool _triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;

        if (other.CompareTag("Player"))
        {
            _triggered = true;
            GameEvents.OnRoomTriggered();
        }
    }
}
