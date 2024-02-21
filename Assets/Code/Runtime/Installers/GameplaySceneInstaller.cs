using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.ObjectPool;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Animation;
using Code.Runtime.Logic.Factories;
using Code.Runtime.Logic.Gameplay;
using Code.Runtime.Services.InputService;
using Code.Runtime.UI;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private ActiveShapeAnimatorsHandler _activeShapeAnimatorsHandler;
        [SerializeField] private ParticleSystem deathVfx;
        [SerializeField] private ParticleSystem tapVfx;
        [SerializeField] private MobileInput mobileInput;

        private GameplayFactoriesInstaller _gameplayFactoriesInstaller;

        public override void InstallBindings()
        {
            BindGameplayStateMachine();

            BindInput();

            BindComboDetector();

            BindShapeDeterminantor();

            BindGlobalGameObjectPool();

            BindActiveShapeAnimatorsHandler();

            BindGameplayFactories();
        }

        private void BindGameplayFactories()
        {
            GameplayFactoriesInstaller.Install(Container, deathVfx, tapVfx);
        }

        private void BindActiveShapeAnimatorsHandler()
        {
            Container.BindInterfacesTo<ActiveShapeAnimatorsHandler>()
                .FromInstance(_activeShapeAnimatorsHandler)
                .AsSingle();
        }

        // Need to refactor
        private void BindGlobalGameObjectPool()
        {
            Container.BindInterfacesTo<GameObjectsPoolContainer>().AsSingle();
        }

        private void BindGameplayStateMachine()
        {
            Container.Bind<GameplayStateMachine>().AsSingle();
        }

        private void BindShapeDeterminantor()
        {
            Container.BindInterfacesTo<ShapeDeterminantor>().AsSingle();
        }

        private void BindComboDetector() =>
            Container.BindInterfacesTo<ComboDetector>().AsSingle();

        private void BindInput() =>
            Container.Bind<IInput>().To<MobileInput>()
                .FromInstance(mobileInput).AsSingle();
    }
}