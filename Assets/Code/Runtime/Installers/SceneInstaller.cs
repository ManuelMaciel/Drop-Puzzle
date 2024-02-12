using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.ObjectPool;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Logic;
using Code.Runtime.UI;
using Code.Services.InputService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private MobileInput mobileInput;

        public override void InstallBindings()
        {
            BindGameplayStateMachine();
            
            BindInput();

            BindComboDetector();
            
            BindStatesFactory();
            
            BindSpawnerFactory();
            
            BindHUDFactory();

            BindShapeDeterminantor();

            BindGlobalGameObjectPool();
        }

        // Need to refactor
        private void BindGlobalGameObjectPool()
        {
            Container.BindInterfacesTo<GlobalGameObjectPool>().AsSingle();
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

        private void BindStatesFactory() =>
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
        
        private void BindSpawnerFactory()
        {
            Container.Bind<Infrastructure.PrefabFactory<Spawner>>().AsSingle();
        }
        
        private void BindHUDFactory()
        {
            Container.Bind<Infrastructure.PrefabFactory<HUD>>().AsSingle();
        }

    }
}