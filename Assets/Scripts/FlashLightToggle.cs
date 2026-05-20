using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class FlashLightToggle : MonoBehaviour
{
    [SerializeField] private Light _flashlightLight;
    [SerializeField] private InputActionReference _toggleAction;



    private void OnEnable()
    { 
        _toggleAction.action.performed += OnToggle;
    }

    private void OnDisable()
    {
        _toggleAction.action.performed -= OnToggle;
    }

    public void OnToggle(InputAction.CallbackContext context)
    {
        _flashlightLight.enabled = !_flashlightLight.enabled;
    }
   
}
