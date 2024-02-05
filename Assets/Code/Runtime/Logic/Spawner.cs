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

        [SerializeField] private Transform dropLine;
        [SerializeField] private Transform spawnPoint;

        private IShapeFactory _shapeFactory;
        private ISaveLoadService _saveLoadService;
        private IShapeDeterminantor _shapeDeterminantor;

        private ShapeDropper _shapeDropper;
        private Transform _currentShapeTransform;
        private int _lastRandomSizeIndex;

        [Inject]
        public void Construct(IShapeFactory shapeFactory, ISaveLoadService saveLoadService,
            IShapeDeterminantor shapeDeterminantor)
        {
            _shapeDeterminantor = shapeDeterminantor;
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

        private void Update()
        {
            if (_currentShapeTransform != null)
                dropLine.position = _currentShapeTransform.position;
        }

        private void SpawnNextShape()
        {
            StartCoroutine(SpawnNextShapeDelay());

            dropLine.gameObject.SetActive(false);
        }

        private IEnumerator SpawnNextShapeDelay()
        {
            yield return new WaitForSeconds(Delay);

            _shapeDropper.AddShape(CreateShape());

            dropLine.gameObject.SetActive(true);
        }

        private Shape CreateShape()
        {
            Shape shape = _shapeFactory.CreateShape(spawnPoint.position, _shapeDeterminantor.GetShape());

            _currentShapeTransform = shape.transform;

            return shape;
        }
    }
}