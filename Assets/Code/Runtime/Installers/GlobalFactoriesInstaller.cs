using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.Bootstrappers;
using Code.Runtime.UI;
using Zenject;

namespace Code.Runtime.Installers
{
    public class GlobalFactoriesInstaller : Installer<GlobalFactoriesInstaller>
    {
        public override void InstallBindings()
        {
            BindStatesFactory();
            
            BindGameBootstrapperFactory();

            BindUIFactory();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle();
        }
        
        private void BindGameBootstrapperFactory()
        {
            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstrapperPath);
        }
        
        private void BindStatesFactory()
        {
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
        }
    }
}