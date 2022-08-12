using System.Collections;
using UnityEngine;

public class MeteoritesGenerator : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _meteoritesObjects;
    [SerializeField] private float _minForce = 250;
    [SerializeField] private float _maxForce = 750;

    IEnumerator StartGenerate()
    {
        while (true)
        {
            float x = Random.Range(GameManager.Game.MinX, GameManager.Game.MaxX);
            int randomIndex = Random.Range(0, _meteoritesObjects.Length);
            float flyForce = Random.Range(_minForce, _maxForce);

            Rigidbody meteorite = _meteoritesObjects[randomIndex];
            Vector3 pos = new Vector3(x, 0, transform.position.z);

            Rigidbody rb = Instantiate(meteorite, pos, Random.rotation);
            rb.AddForce(Vector3.back * flyForce);
            yield return new WaitForSeconds(GameManager.Game.GenerationOffset);
        }
    }

    private void Start()
    {
        StartCoroutine(nameof(StartGenerate));
    }
}
