using UnityEngine;

public class Test_cube : MonoBehaviour, IInteractable
{
    [SerializeField] private float _attractRadius = 25f;
    [SerializeField] private float _investigateDuration = 5f;
    [SerializeField] private string _distractionTag = "Distraction";
    [SerializeField] private LayerMask _enemyLayer = ~0;


    //--------------------------------------------
    // Implementación de la interfaz IInteractable
    //--------------------------------------------

    public void Interact()
    {
        gameObject.tag = _distractionTag;
        Debug.Log("Objeto interactuado");

        
        Collider[] cols = Physics.OverlapSphere(transform.position, _attractRadius, _enemyLayer);
        foreach (var col in cols)
        {
            var enemy = col.GetComponentInParent<Mov_Enemy>();
            if (enemy != null)
            {
                enemy.Investigate(transform.position, _investigateDuration);
            }
        }
    }


    // Metodo para visualizar el radio de attraccion en el editor

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attractRadius);
    }
}
