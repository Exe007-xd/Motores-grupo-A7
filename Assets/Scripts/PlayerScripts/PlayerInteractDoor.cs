using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteractDoors : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private LayerMask doorLayer;

    [Header("UI (Opcional)")]
    [SerializeField] private TextMeshProUGUI promptText;

    private DoorController _nearbyDoor;

    private PlayerMove inputActions;

    private void Awake()
    {
        inputActions = new PlayerMove();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Disable();
    }

    private void Update()
    {
        DetectDoor();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (_nearbyDoor != null)
        {
            _nearbyDoor.Interact();
        }
    }

    private void DetectDoor()
    {
        DoorController[] allDoors =
            FindObjectsByType<DoorController>(FindObjectsSortMode.None);

        _nearbyDoor = null;

        foreach (DoorController door in allDoors)
        {
            float distance = Vector3.Distance(
                transform.position,
                door.transform.position);

            if (distance <= door.GetInteractDistance())
            {
                _nearbyDoor = door;
                break;
            }
        }

        if (promptText != null)
            promptText.gameObject.SetActive(_nearbyDoor != null);
    }
}