using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Configuracion de la Puerta")]
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 3f;
    [SerializeField] private float interactDistance = 2.5f;

    [Header("Audio (Opcional)")]
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private AudioSource _audioSource;

    private bool _isOpen = false;
    private bool _isMoving = false;

    private void Start()
    {
        // Guardamos la rotacion inicial (cerrada)
        _closedRotation = transform.rotation;

        // Calculamos la rotacion abierta
        _openRotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + openAngle,
            transform.eulerAngles.z
        );

        // Buscamos o agregamos AudioSource
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (_isMoving)
            AnimateDoor();
    }

    // Llamado desde PlayerInteract
    public void Interact()
    {
        _isOpen = !_isOpen;
        _isMoving = true;

        // Reproducir sonido
        if (_audioSource != null)
        {
            AudioClip clip = _isOpen ? openSound : closeSound;
            if (clip != null)
                _audioSource.PlayOneShot(clip);
        }
    }

    private void AnimateDoor()
    {
        Quaternion targetRotation = _isOpen ? _openRotation : _closedRotation;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * openSpeed
        );

        // Detener cuando llega al destino
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
        {
            transform.rotation = targetRotation;
            _isMoving = false;
        }
    }

    // Getter para que PlayerInteract sepa la distancia de interaccion
    public float GetInteractDistance() => interactDistance;
}
