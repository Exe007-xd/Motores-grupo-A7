using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Interact : MonoBehaviour
{
    private float _interactRange = 4f;

        //--------------------------------------------
        //Metodo para que el jugador pueda interactuar
        //--------------------------------------------
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                RaycastHit hit;
                Debug.DrawRay(transform.position, transform.forward * _interactRange, Color.red, 1f);
            if (Physics.Raycast(transform.position, transform.forward, out hit, _interactRange))
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    //IPickable pickable = hit.collider.GetComponent<IPickable>();
                if (interactable != null)
                    {
                        interactable.Interact();
                        //sin implementar todavia ya que falta sistema de inventario.
                        //if (pickable != null)
                        //{
                        //    pickable.PickUp();
                        //}
                    }
                }
            }
    }
}
