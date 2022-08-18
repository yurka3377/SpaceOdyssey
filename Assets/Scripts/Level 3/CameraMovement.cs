using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothingSpeed = 10f;
    [SerializeField] private Vector3 _offset;

    private void FixedUpdate()
    {
        Vector3 targetPos = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, _smoothingSpeed * Time.deltaTime);
        transform.LookAt(_target);
    }
}