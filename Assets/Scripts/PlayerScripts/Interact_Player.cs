using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Interact : MonoBehaviour
{
    private float _interactRange = 4f;
    [SerializeField] private Camera _playerCamera;
    //--------------------------------------------
    //Metodo para que el jugador pueda interactuar
    //--------------------------------------------
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RaycastHit hit;
            Vector3 origin = _playerCamera.transform.position;
            Vector3 direction = _playerCamera.transform.forward;

            Debug.DrawRay(origin, direction * _interactRange, Color.red, 1f);
            if (Physics.Raycast(origin, direction, out hit, _interactRange))
            {
                IInteractable interactable =
                    hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}
