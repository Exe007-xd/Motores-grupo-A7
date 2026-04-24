using UnityEngine;

public class Detection_Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _defaultInvestigateDuration = 5f;

    private Mov_Enemy _enemyAI;

    private void Awake()
    {
        if (_enemy != null)
            _enemyAI = _enemy.GetComponent<Mov_Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_enemyAI != null)
                _enemyAI.StartChasing();
        }

        if (other.CompareTag("Distraction"))
        {
            if (_enemyAI != null)
                _enemyAI.Investigate(other.transform.position, _defaultInvestigateDuration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_enemyAI != null)
                _enemyAI.StopChasing();
        }
    }
}
