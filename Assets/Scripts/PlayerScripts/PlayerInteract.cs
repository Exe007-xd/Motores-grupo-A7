using UnityEngine;
using TMPro; // Requiere TextMeshPro

public class PlayerInteract : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private LayerMask doorLayer;

    [Header("UI (Opcional)")]
    [SerializeField] private TextMeshProUGUI promptText; // Texto "Presiona E"

    private DoorController _nearbyDoor = null;

    private void Update()
    {
        DetectDoor();

        if (_nearbyDoor != null && Input.GetKeyDown(interactKey))
        {
            _nearbyDoor.Interact();
        }
    }

    private void DetectDoor()
    {
        // Buscamos todas las puertas en escena
        DoorController[] allDoors = FindObjectsByType<DoorController>(FindObjectsSortMode.None);

        _nearbyDoor = null;

        foreach (DoorController door in allDoors)
        {
            float distance = Vector3.Distance(transform.position, door.transform.position);

            if (distance <= door.GetInteractDistance())
            {
                _nearbyDoor = door;
                break;
            }
        }

        // Mostrar/ocultar prompt UI
        if (promptText != null)
            promptText.gameObject.SetActive(_nearbyDoor != null);
    }
}
