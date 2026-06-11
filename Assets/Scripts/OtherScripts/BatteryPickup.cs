using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public float rechargeAmount = 50f;
    public string playerTag = "Player";

    private bool playerInRange = false;
    private Transform playerTransform;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // intenta encontrar la linterna en la cámara del jugador
            Camera cam = Camera.main;
            if (cam != null)
            {
                FlashlightController fl = cam.GetComponentInChildren<FlashlightController>();
                if (fl != null)
                {
                    fl.Recharge(rechargeAmount);
                    Destroy(gameObject); // consumida
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
            playerTransform = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
            playerTransform = null;
        }
    }
}
