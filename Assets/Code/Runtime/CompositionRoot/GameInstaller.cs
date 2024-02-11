using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Installers;
using Code.Services.Progress;
using Code.Services.SaveLoadService;
using Code.Services.StaticDataService;
using Code.Services.WindowsService;
using CodeBase.Services.LogService;
using Zenject;

namespace Code.Runtime.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
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
        
            BindFactories();
        }

        private void BindWindowService()
        {
            Container.BindInterfacesTo<WindowService>().AsSingle();
        }

        private void BindSaveLoadService()
        {
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
        }

        private void BindFactories()
        {
            FactoriesInstaller.Install(Container);
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