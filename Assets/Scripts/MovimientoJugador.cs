using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadRotacion;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform transformPersonaje;
    [SerializeField] private Camera camaraPersonaje;

    private Vector3 movimiento;
    private float rotacionX;

    private void Update()
    {
        MovimientoDelJugador();
        MovimientoDeCamara();
    }

    void MovimientoDelJugador()
    {
        float movX = Input.GetAxis("Horizontal");
        float movZ = Input.GetAxis("Vertical");

        movimiento = transform.right * movX + transform.forward * movZ;
            characterController.SimpleMove(movimiento * velocidadMovimiento);
    }
    void MovimientoDeCamara()
    {
        float ratonX = Input.GetAxis("Mouse X") * velocidadRotacion;
        float ratonY = Input.GetAxis("Mouse Y") * velocidadRotacion;

        rotacionX -= ratonY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        camaraPersonaje.transform.localRotation = Quaternion.Euler(rotacionX, 0, 0);
        transformPersonaje.Rotate(Vector3.up * ratonX);
    }
}