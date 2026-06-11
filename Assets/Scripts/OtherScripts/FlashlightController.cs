using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [Header("Attach")]
    public Transform holdParent; // asignar la cámara o punto donde quedar sujeta
    public Vector3 holdPosition = new Vector3(0.2f, -0.2f, 0.5f);
    public Vector3 holdRotation = Vector3.zero;

    [Header("Light & Battery")]
    public Light flashlightLight;
    public float maxBattery = 100f;
    public float battery;
    public float drainPerSecond = 10f;
    public float rechargeAmountPerBattery = 50f;

    private bool isHeld = false;
    private bool isOn = false;

    void Start()
    {
        if (flashlightLight == null)
        {
            flashlightLight = GetComponentInChildren<Light>();
        }
        battery = Mathf.Clamp(battery == 0 ? maxBattery : battery, 0, maxBattery);
        UpdateLightState();
    }

    void Update()
    {
        // Toggle encendido/apagado con F solo si la linterna está en mano
        if (isHeld && Input.GetKeyDown(KeyCode.F))
        {
            if (battery > 0f)
            {
                isOn = !isOn;
                UpdateLightState();
            }
        }

        // Drenaje cuando está encendida
        if (isOn)
        {
            battery -= drainPerSecond * Time.deltaTime;
            if (battery <= 0f)
            {
                battery = 0f;
                isOn = false;
                UpdateLightState();
            }
        }
    }

    void UpdateLightState()
    {
        if (flashlightLight != null)
            flashlightLight.enabled = isOn;
    }

    // Llamar para que el jugador recoja la linterna (por ejemplo desde PlayerInteractor o al colisionar)
    public void PickUp()
    {
        if (holdParent == null)
        {
            // intenta buscar la cámara principal
            Camera cam = Camera.main;
            if (cam != null) holdParent = cam.transform;
        }

        transform.SetParent(holdParent);
        transform.localPosition = holdPosition;
        transform.localEulerAngles = holdRotation;
        transform.localScale = Vector3.one;
        isHeld = true;

        // desactivar rigidbody físicas si existen para que no se caiga
        if (TryGetComponent<Rigidbody>(out var rb)) rb.isKinematic = true;
        if (TryGetComponent<Collider>(out var col)) col.enabled = false;
    }

    // Método público para recargar batería
    public void Recharge(float amount)
    {
        battery = Mathf.Clamp(battery + amount, 0f, maxBattery);
    }

    // Opcional: devolver estado para UI
    public float GetBatteryPercent()
    {
        return battery / maxBattery;
    }
}
