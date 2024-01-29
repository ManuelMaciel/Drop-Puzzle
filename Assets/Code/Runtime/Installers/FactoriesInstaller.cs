using Code.Runtime.Infrastructure;
using Code.Runtime.Logic;
using Code.Runtime.UI;
using Zenject;

namespace Code.Runtime.Installers
{
    public class FactoriesInstaller : Installer<FactoriesInstaller>
    {
        public override void InstallBindings()
        {
            BindStatesFactory();
            
            BindGameBootstraperFactory();
            
            BindShapeFactory();
            
            BindSpawnerFactory();

            BindHUDFactory();
            
            BindUIFactory();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle();
        }

        private void BindSpawnerFactory()
        {
            Container
                .BindFactory<string, Spawner, Spawner.Factory>()
                .FromFactory<Code.Runtime.Infrastructure.PrefabFactory<Spawner>>();
        }

        private void BindHUDFactory()
        {
            Container
                .BindFactory<string, HUD, HUD.Factory>()
                .FromFactory<Code.Runtime.Infrastructure.PrefabFactory<HUD>>();
        }

        private void BindGameBootstraperFactory()
        {
            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstraperPath);
        }

        private void BindShapeFactory()
        {
            Container.BindInterfacesTo<ShapeFactory>().AsSingle();
        }

        private void BindStatesFactory()
        {
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
        }
    }
}