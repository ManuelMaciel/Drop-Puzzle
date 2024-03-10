using System.Collections;
using Code.Runtime.Configs;
using Code.Runtime.Logic.Factories;
using Code.Runtime.Services.SaveLoadService;
using Code.Runtime.Services.StaticDataService;
using Plugin.DocuFlow.Documentation;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Gameplay
{
    [Doc("The ShapeSpawner class is responsible for spawning shapes in the gameplay scene at regular intervals. It uses a ShapeFactory to create shapes based on a predefined spawn point and shape size determined by a ShapeDeterminantor.")]
    [RequireComponent(typeof(ShapeDropper))]
    public class ShapeSpawner : MonoBehaviour
    {
        [SerializeField] private Transform dropLine;

        private IShapeFactory _shapeFactory;
        private ISaveLoadService _saveLoadService;
        private IShapeDeterminantor _shapeDeterminantor;

        private ShapeDropper _shapeDropper;
        private Transform _currentShapeTransform;
        private GameplayConfig _gameplayConfig;
        private int _lastRandomSizeIndex;

        [Inject]
        public void Construct(IShapeFactory shapeFactory, ISaveLoadService saveLoadService,
            IShapeDeterminantor shapeDeterminantor, IStaticDataService staticDataService)
        {
            _shapeDeterminantor = shapeDeterminantor;
            _saveLoadService = saveLoadService;
            _shapeFactory = shapeFactory;
            _gameplayConfig = staticDataService.GameplayConfig;
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
            yield return new WaitForSeconds(_gameplayConfig.DropInterval);

            _shapeDropper.AddShape(CreateShape());

            dropLine.gameObject.SetActive(true);
            _saveLoadService.SaveProgress();
        }

        private Shape CreateShape()
        {
            Shape shape = _shapeFactory.CreateShape(_gameplayConfig.SpawnPointPosition, _shapeDeterminantor.GetShape());

            _currentShapeTransform = shape.transform;

            return shape;
        }
    }
}