using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Installers;
using Code.Runtime.Services.AdsService;
using Code.Runtime.Services.AudioService;
using Code.Runtime.Services.LogService;
using Code.Runtime.Services.Progress;
using Code.Runtime.Services.SaveLoadService;
using Code.Runtime.Services.StaticDataService;
using Code.Runtime.Services.WindowsService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AudioService _audioService;
        
        public override void InstallBindings()
        {
            BindCoroutineRunner();
        
            BindGameStateMachine();

            BindLogService();

            BindSceneLoader();
        
            BindPersistentProgress();

            BindStaticDataService();
            
            BindSaveLoadService();

            BindWindowService();
        
            BindGlobalFactories();

            BindAudioService();

            AdsService();
        }

        private void AdsService()
        {
            Container.Bind<IAdsService>().To<AdsService>().AsSingle();
        }

        private void BindAudioService()
        {
            Container.BindInterfacesTo<AudioService>()
                .FromInstance(_audioService)
                .AsSingle();
        }

        private void BindWindowService()
        {
            Container.BindInterfacesTo<WindowService>().AsSingle();
        }

        private void BindSaveLoadService()
        {
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
        }

        private void BindGlobalFactories()
        {
            GlobalFactoriesInstaller.Install(Container);
        }

        private void BindStaticDataService()
        {
            Container.BindInterfacesTo<StaticDataService>().AsSingle();
        }
    
        private void BindPersistentProgress()
        {
            Container.BindInterfacesTo<PersistentProgressService>().AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
        }

        private void BindLogService()
        {
            Container.BindInterfacesTo<LogService>().AsSingle();
        }
    
        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>().AsSingle();
        }
    
        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunnerPath)
                .AsSingle();
        }
    }
}