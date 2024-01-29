using System.Collections;
using Code.Services.SaveLoadService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(Movement))]
    public class Spawner : MonoBehaviour
    {
        private const float Delay = 1f;

        [SerializeField] private Transform spawnPoint;

        private IShapeFactory _shapeFactory;
        private ISaveLoadService _saveLoadService;

        private Movement _movement;

        [Inject]
        public void Construct(IShapeFactory shapeFactory, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
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

        private Shape CreateShape()
        {
            ShapeSize shapeSize = (ShapeSize) Random.Range(0, 2);
            Shape shape = _shapeFactory.CreateShape(spawnPoint.position, shapeSize);
            
            return shape;
        }

        public class Factory : PlaceholderFactory<string, Spawner>
        {
        }
    }
}