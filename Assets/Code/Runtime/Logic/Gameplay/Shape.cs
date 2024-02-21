using System;
using Code.Runtime.Configs;
using Code.Runtime.Extensions;
using Code.Runtime.Infrastructure.ObjectPool;
using Code.Runtime.Interactors;
using Code.Services.AudioService;
using Code.Services.Progress;
using Code.Services.SaveLoadService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Gameplay
{
    public class Shape : MonoBehaviour, IShapeBase
    {
    public ShapeSize ShapeSize { get; private set; }
    public int CombinedCount { get; private set; }
    public string ShapeId { get; private set; }


    [Inject] private IAudioService _audioService;
    private IShapeFactory _shapeFactory;
    private ShapeInteractor _shapeInteractor;
    private ScoreInteractor _scoreInteractor;
    private GameplayShapesInteractor _gameplayShapesInteractor;
    private IGameObjectPool<Shape> _shapePool;
    private Rigidbody2D _rigidbody;
    private Action _onDestroyed;
    private ISaveLoadService _saveLoadService;

    private void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void Construct(IShapeFactory shapeFactory,
        IPersistentProgressService progressService, ISaveLoadService saveLoadService,
        IGameObjectPool<Shape> shapePool)
    {
        _shapePool = shapePool;
        _saveLoadService = saveLoadService;
        _scoreInteractor = progressService.InteractorContainer.Get<ScoreInteractor>();
        _shapeInteractor = progressService.InteractorContainer.Get<ShapeInteractor>();
        _gameplayShapesInteractor = progressService.InteractorContainer.Get<GameplayShapesInteractor>();

        _shapeFactory = shapeFactory;
    }

    public void Initialize(ShapeSize shapeSize, string shapeId = null)
    {
        ShapeSize = shapeSize;
        ShapeId = shapeId;
        CombinedCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(CombinedCount >= 1) return;
        
        if (other.gameObject.TryGetComponent(out Shape shape))
        {
            if (shape.CombinedCount == 0 && shape.ShapeSize == this.ShapeSize)
            {
                Vector3 spawnPosition = this.transform.position;
                CombinedCount++;
                
                _audioService.PlaySfx(SfxType.CombineShape);
                _shapeFactory.CreateShape(spawnPosition, ShapeSize.NextSize(), true);
                _scoreInteractor.AddScoreByShapeSize(ShapeSize);
                _shapeInteractor.ShapeCombined(shape);
                
                DestroyShape(this);
                DestroyShape(shape);
            }
        }
    }

    private void DestroyShape(Shape shape)
    {
        _saveLoadService.RemoveUpdatebleProgress(shape);
        _gameplayShapesInteractor.RemoveShape(shape);
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0f;
        _shapePool.Return(shape);
        _onDestroyed?.Invoke();
    }

    public void UpdateProgress(IPersistentProgressService persistentProgressService) =>
        persistentProgressService.InteractorContainer.Get<GameplayShapesInteractor>().UpdateShapeData(this);
    }
}