using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LandingControll : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private void Start()
    {
        float x = Random.Range(-1, 1);
        float y = Random.Range(-1, 1);
        float z = Random.Range(-1, 1);
        Vector3 randomDirection = new Vector3(x, y, z);
        //_rigidbody.AddTorque(randomDirection, ForceMode.Impulse);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up);
        }
        transform.Rotate(Vector3.forward, Input.GetAxisRaw("Horizontal") * 30 * Time.deltaTime);
        transform.Rotate(Vector3.right, Input.GetAxisRaw("Vertical") * 30 * Time.deltaTime);
#endif
    }

    private void OnCollisionEnter(Collision collision)
    {
        float angle = Vector3.Angle(collision.contacts[0].normal, transform.up);
        if (collision.relativeVelocity.magnitude <= 10 && angle < 44) 
        {
            Debug.Log("Победа");
        }
        else Debug.Log("Разбился");
    }
}
