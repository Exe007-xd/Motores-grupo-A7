using UnityEngine;

public class Mov_Enemy : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float _speed = 5f;

    // Variables para el estado de investigacion
    private Vector3 _investigateTarget;
    private float _investigateTimer;
    [SerializeField] private float _stopDistance = 0.5f;

    private enum State { Idle, Chase, Investigate }
    private State _state = State.Idle;

    [SerializeField] private float _groundCheckDistance = 10f;
    private float _desiredGroundY = float.NaN;
    private bool _groundDetected = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        // Si este objeto tiene la etiqueta 'Enemy', colocarlo en el punto de spawn indicado
        if (CompareTag("Enemy"))
        {
            Vector3 desiredPos = new Vector3(-7.565056f, 3.3f, 11.25876f);

            // Raycast hacia abajo desde un poco por encima para ubicar el suelo y evitar que flote
            RaycastHit hit;
            Vector3 rayStart = desiredPos + Vector3.up * 5f;
            if (Physics.Raycast(rayStart, Vector3.down, out hit, 50f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                // Ajustar la Y para apoyar sobre el punto de impacto teniendo en cuenta la posici�n actual
                // y la m�nima Y del collider (permite manejar pivotes desplazados)
                Collider col = GetComponent<Collider>();
                if (col != null)
                {
                    // distancia vertical entre la posici�n actual del transform y la parte inferior del collider
                    float bottomOffset = transform.position.y - col.bounds.min.y;
                    desiredPos.y = hit.point.y + bottomOffset;
                }
                else
                {
                    desiredPos.y = hit.point.y;
                }
            }

            transform.position = desiredPos;
        }
    }

    void Update()
    {
        // Detectar el suelo continuamente
        DetectGround();

        // Si no hay Rigidbody, aplicar la correcci�n de altura inmediatamente
        if (!TryApplyGroundToRigidbody())
        {
            if (_groundDetected && !float.IsNaN(_desiredGroundY))
            {
                Vector3 p = transform.position;
                p.y = _desiredGroundY;
                transform.position = p;
            }
        }

        switch (_state)
        {
            case State.Chase:
                if (_player != null)
                    MoveTowards(_player.transform.position);
                break;

            case State.Investigate:
                _investigateTimer -= Time.deltaTime;
                MoveTowards(_investigateTarget);
                // comprobar si lleg� al objetivo o se le acab� el tiempo
                if (Vector3.Distance(transform.position, _investigateTarget) <= _stopDistance || _investigateTimer <= 0f)
                {
                    // volver a perseguir jugador si lo conoce (o a Idle)
                    if (_player != null)
                        StartChasing();
                    else
                        _state = State.Idle;
                }
                break;
            // en Idle no hace nada
            case State.Idle:
                break;
        }
    }

    private void FixedUpdate()
    {
        // Si hay Rigidbody, aplicar la correcci�n de altura en FixedUpdate usando MovePosition
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null && _groundDetected && !float.IsNaN(_desiredGroundY))
        {
            Vector3 np = rb.position;
            np.y = _desiredGroundY;
            rb.MovePosition(np);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        }
    }

    private void DetectGround()
    {
        _groundDetected = false;
        _desiredGroundY = float.NaN;

        Vector3 rayStart = transform.position + Vector3.up * 1f;
        RaycastHit hit;
        if (Physics.Raycast(rayStart, Vector3.down, out hit, _groundCheckDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                float bottomOffset = transform.position.y - col.bounds.min.y;
                _desiredGroundY = hit.point.y + bottomOffset;
            }
            else
            {
                _desiredGroundY = hit.point.y;
            }
            _groundDetected = true;
        }
    }

    private bool TryApplyGroundToRigidbody()
    {
        // devuelve true si existe Rigidbody (se aplicar� en FixedUpdate)
        return GetComponent<Rigidbody>() != null;
    }

    private void MoveTowards(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
       
    }
    //--------------------------------------
    // m�todos p�blicos para controlar el AI
    //--------------------------------------
    public void Investigate(Vector3 position, float duration)
    {
        _investigateTarget = position;
        _investigateTimer = duration;
        _state = State.Investigate;
    }

    public void StartChasing()
    {
        _state = State.Chase;
    }

    public void StopChasing()
    {
        _state = State.Idle;
    }
}
