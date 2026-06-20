using UnityEngine;

public class BoardedDoor : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;


    private void OnEnable()
    {
        GameEvents.onRoomTriggered += DestroyDoor;
    }

    private void OnDisable()
    {
        GameEvents.onRoomTriggered -= DestroyDoor;
    }

    private void DestroyDoor()
    {
        Destroy(gameObject);
        if (_enemy != null)
        {
           _enemy.SetActive(true);
        }
    }
}
