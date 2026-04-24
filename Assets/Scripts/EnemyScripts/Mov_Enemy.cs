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

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Update()
    {
        
        switch (_state)
        {
            case State.Chase:
                if (_player != null)
                    MoveTowards(_player.transform.position);
                break;

            case State.Investigate:
                _investigateTimer -= Time.deltaTime;
                MoveTowards(_investigateTarget);
                // comprobar si llegó al objetivo o se le acabó el tiempo
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

    private void MoveTowards(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
       
    }
    //--------------------------------------
    // métodos públicos para controlar el AI
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
