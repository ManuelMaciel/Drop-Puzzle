using System.Collections;
using Code.Runtime;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject shapePrefab;
    [SerializeField] private Transform spawnPoint;

    private Movement _movement;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _movement.AddShape(CreateShape());

        _movement.OnShapeDropped += SpawnNextShape;
    }

    private void OnDestroy() => 
        _movement.OnShapeDropped += SpawnNextShape;

    private void SpawnNextShape() => 
        StartCoroutine(SpawnNextShapeDelay());

    private IEnumerator SpawnNextShapeDelay()
    {
        yield return new WaitForSeconds(2f);
        
        _movement.AddShape(CreateShape());
    }

    private Rigidbody2D CreateShape()
    {
        GameObject instantiate = Instantiate(shapePrefab, spawnPoint);
        Rigidbody2D shapeRigidbody = instantiate.GetComponent<Rigidbody2D>();

        float randomSize = Random.Range(0.3f, 1.5f);

        shapeRigidbody.transform.localScale = new Vector2(randomSize, randomSize);
        shapeRigidbody.bodyType = RigidbodyType2D.Kinematic;

        return shapeRigidbody;
    }
}