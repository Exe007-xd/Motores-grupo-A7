using UnityEngine;
using UnityEngine.InputSystem;

public class Throwing_Player : MonoBehaviour
{
    //--------------------
    //Refencias a objetos
    //--------------------
   [SerializeField] private Transform cam;
   [SerializeField] private Transform attackPoint;
   [SerializeField] private GameObject projectilePrefab;

    //----------
    //Variables
    //----------

    [SerializeField] private float _throwForce = 10f;
    private float _verticalThrowForce = 1f;
    private bool _canThrow = true;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.started && _canThrow)
        {
            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, cam.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            Vector3 throwDirection = cam.forward + Vector3.up * _verticalThrowForce;
            rb.AddForce(throwDirection.normalized * _throwForce, ForceMode.Impulse);
        }
    }
}
