using System.Collections;
using UnityEngine;

namespace Code.Runtime
{
    //ShapeDropper
    [RequireComponent(typeof(Movement))]
    public class Spawner : MonoBehaviour
    {
        private const float Delay = 1f;
        
        [SerializeField] private GameObject shapePrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private ShapeFactory shapeFactory;

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
            yield return new WaitForSeconds(Delay);
        
            _movement.AddShape(CreateShape());
        }

        private Rigidbody2D CreateShape()
        {
            ShapeSize shapeSize = (ShapeSize)Random.Range(0, 2);
            Shape shape = shapeFactory.CreateShape(spawnPoint.position, shapeSize);
            Rigidbody2D shapeRigidbody = shape.GetComponent<Rigidbody2D>();

            shapeRigidbody.bodyType = RigidbodyType2D.Kinematic;

            return shapeRigidbody;
        }
    }
}