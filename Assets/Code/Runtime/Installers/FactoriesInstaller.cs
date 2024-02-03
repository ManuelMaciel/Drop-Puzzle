﻿using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.Bootstrappers;
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
            
            BindGameBootstrapperFactory();
            
            BindShapeFactory();

            BindHUDFactory();
            
            BindUIFactory();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle();
        }
        
        private void BindHUDFactory()
        {
            Container
                .BindFactory<string, HUD, HUD.Factory>()
                .FromFactory<Infrastructure.PrefabFactory<HUD>>();
        }

        private void BindGameBootstrapperFactory()
        {
            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstrapperPath);
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