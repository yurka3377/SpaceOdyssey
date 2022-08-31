using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LandingControll : MonoBehaviour
{
    [SerializeField] private float _thrustForce = 3;
    [SerializeField] private float _angleRotation = 30;
    [SerializeField] private Rigidbody _rigidbody;

    private float _horizontal;
    private float _vertical;

    private ArduinoControl ac;
    private GameObject al;


    private void Start()
    {
        float x = Random.Range(-1, 1);
        float y = Random.Range(-1, 1);
        float z = Random.Range(-1, 1);
        Vector3 randomDirection = new Vector3(x, y, z) * 500;
        //_rigidbody.AddTorque(randomDirection, ForceMode.Impulse);
        al = GameObject.Find("ArduinoLink"); 
        ac = al.GetComponent<ArduinoControl>();
        if (ac != null){
            ac.setLevel(3);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical  = Input.GetAxisRaw("Vertical");
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    _rigidbody.AddRelativeForce(Vector3.up * _thrustForce);
        //}
#endif
        transform.Rotate(transform.forward, _horizontal * _angleRotation * Time.deltaTime);
        transform.Rotate(transform.right, _vertical * _angleRotation * Time.deltaTime);
    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        if (al == null){
            al = GameObject.Find("ArduinoLink"); 
            ac = al.GetComponent<ArduinoControl>();
        }
        if (ac != null && ac.available() > 0){
            byte d = ac.getData();
            if (d > 7){
                Debug.Log("up!");
                _rigidbody.AddRelativeForce(Vector3.up * _thrustForce);
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * _thrustForce);
        }
#endif
    }
}
