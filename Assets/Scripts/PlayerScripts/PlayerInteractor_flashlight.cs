using UnityEngine;

public class PlayerInteractor_flashlight : MonoBehaviour
{
    public float interactRange = 2f;
    public LayerMask interactLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
            {
                // si golpea una linterna
                FlashlightController fl = hit.collider.GetComponentInParent<FlashlightController>();
                if (fl != null)
                {
                    fl.PickUp();
                }
            }
        }
    }
}
