using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] destinations;
    [SerializeField] private float distanceToFollowPlayer = 5f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float noiseDistance = 20f;

    private int currentDestination = 0;

    private float waitCounter;
    private bool waiting;
    private bool _investigatingNoise;
    private Vector3 _noisePosition;
    

    void Start()
    {
        navMeshAgent.SetDestination(destinations[currentDestination].transform.position);
    }


    void Update()
    {
        // =========================
        // ESPERA
        // =========================

        if (waiting)
        {
            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                waiting = false;

                currentDestination++;

                if (currentDestination >= destinations.Length)
                {
                    currentDestination = 0;
                }

                navMeshAgent.SetDestination(
                    destinations[currentDestination].transform.position
                );
            }

            return;
        }

        // =========================
        // DETECCIÓN JUGADOR
        // =========================

        if (Vector3.Distance(player.transform.position, transform.position) < distanceToFollowPlayer)
        {
            navMeshAgent.SetDestination(player.transform.position);
            return;
        }
        // =========================
        // DETECCIÓN RUIDO
        // =========================
        if (_investigatingNoise)
        {
            if (!navMeshAgent.pathPending &&
                navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                _investigatingNoise = false;

                navMeshAgent.SetDestination(
                    destinations[currentDestination].transform.position
                );
            }

            return;
        }

        // =========================
        // DETECTA LLEGADA
        // =========================

        if (!navMeshAgent.pathPending &&
            navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance &&
            (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f))
        {
            waiting = true;
            waitCounter = waitTime;
        }


    }

    private void OnDrawGizmos()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Cambia de color si detecta al jugador
        if (distance < distanceToFollowPlayer)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        // Radio de detección
        Gizmos.DrawWireSphere(transform.position, distanceToFollowPlayer);

        // Línea hacia el jugador
        Gizmos.DrawLine(transform.position, player.transform.position);

        //Radio de detección de ruido
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, noiseDistance);
    }

    private void OnEnable()
    {
        NoiseSystem.OnNoiseMade += InvestigateNoise;
    }

    private void OnDisable()
    {
        NoiseSystem.OnNoiseMade -= InvestigateNoise;
    }

    private void InvestigateNoise( Vector3 position, float radius)
    {
        float distance =
            Vector3.Distance(transform.position, position);

        if (distance > radius)
            return;

        _investigatingNoise = true;
        _noisePosition = position;

        navMeshAgent.SetDestination(position);
    }



}