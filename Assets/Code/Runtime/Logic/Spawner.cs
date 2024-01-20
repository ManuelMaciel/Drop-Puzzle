using System.Collections;
using Code.Runtime.Logic;
using UnityEngine;
using Zenject;

namespace Code.Runtime
{
    //ShapeDropper
    [RequireComponent(typeof(Movement))]
    public class Spawner : MonoBehaviour
    {
        private const float Delay = 1f;

        [SerializeField] private Transform spawnPoint;

        private IShapeFactory _shapeFactory;
        private Movement _movement;

        [Inject]
        public void Construct(IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
        }

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
            Shape shape = _shapeFactory.CreateShape(spawnPoint.position, shapeSize);
            Rigidbody2D shapeRigidbody = shape.GetComponent<Rigidbody2D>();

            shapeRigidbody.bodyType = RigidbodyType2D.Kinematic;

            return shapeRigidbody;
        }
    }
}