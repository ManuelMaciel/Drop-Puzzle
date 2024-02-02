using System.Collections;
using Code.Services.SaveLoadService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(ShapeDropper))]
    public class Spawner : MonoBehaviour
    {
        private const float Delay = 1f;

        [SerializeField] private Transform spawnPoint;

        private IShapeFactory _shapeFactory;
        private ISaveLoadService _saveLoadService;

        private ShapeDropper _shapeDropper;
        private int _lastRandomSizeIndex;

        [Inject]
        public void Construct(IShapeFactory shapeFactory, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _shapeFactory = shapeFactory;
        }

        private void Awake()
        {
            _shapeDropper = GetComponent<ShapeDropper>();

            _shapeDropper.Initialize();
            _shapeDropper.AddShape(CreateShape());

            _shapeDropper.OnShapeDropped += SpawnNextShape;
        }

        private void OnDestroy() =>
            _shapeDropper.OnShapeDropped += SpawnNextShape;

        private void SpawnNextShape() =>
            StartCoroutine(SpawnNextShapeDelay());

        private IEnumerator SpawnNextShapeDelay()
        {
            yield return new WaitForSeconds(Delay);

            _shapeDropper.AddShape(CreateShape());
        }

        private Shape CreateShape()
        {
            ShapeSize shapeSize = GetRandomShape();
            Shape shape = _shapeFactory.CreateShape(spawnPoint.position, shapeSize);

            return shape;
        }

        private ShapeSize GetRandomShape()
        {
            int randomIndex = 0;

            do
            {
                randomIndex = Random.Range(0, 4);
            } while (_lastRandomSizeIndex == randomIndex);

            return (ShapeSize) (_lastRandomSizeIndex = randomIndex);
        }

        public class Factory : PlaceholderFactory<string, Spawner>
        {
        }
    }
}