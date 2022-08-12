using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushChecker : MonoBehaviour
{
    [SerializeField] private GameObject Model;
    [SerializeField] private GameObject CrushModel;

    [SerializeField] private const float _crushAngle = 44;
    [SerializeField] private float _limitForce = 10;

    private void OnCollisionEnter(Collision collision)
    {
        float angle = Vector3.Angle(collision.contacts[0].normal, transform.up);
        if (collision.relativeVelocity.magnitude <= _limitForce && angle < _crushAngle)
        {
            Debug.Log("Победа");
        }
        else
        {
            Debug.Log("Разбился");
            Crush();
            if (Camera.main.TryGetComponent<CameraMovement>(out CameraMovement camera))
            {
                Destroy(camera);
            }
        }
        //Destroy(this);
    }

    public void Crush()
    {
        Model.SetActive(false);
        CrushModel.SetActive(true);
    }
}
