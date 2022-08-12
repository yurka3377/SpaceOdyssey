using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement2D : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 3f;
    [SerializeField] private float _tilt = 3f;
    
    private float _currentSpeed = 0;
    private Vector3 _direction = Vector3.zero;


    private void Update()
    {
#if UNITY_EDITOR
        _currentSpeed = Input.GetAxis("Horizontal") * _maxSpeed;
#endif
        _direction = Vector3.right * _currentSpeed;
        transform.Translate(_direction * Time.deltaTime);
        float x = Mathf.Clamp(transform.position.x, GameManager.Game.MinX, GameManager.Game.MaxX);
        _direction.x = x;
        _direction.z = GameManager.Game.Z;
        transform.position = _direction;
        transform.rotation = Quaternion.Euler(Vector3.back * _currentSpeed * _tilt);
    }
}