using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Interact : MonoBehaviour
{
    private float _interactRange = 2f;

    

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                RaycastHit hit;
                Debug.DrawRay(transform.position, transform.forward * _interactRange, Color.red, 1f);
            if (Physics.Raycast(transform.position, transform.forward, out hit, _interactRange))
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }
    }
}
