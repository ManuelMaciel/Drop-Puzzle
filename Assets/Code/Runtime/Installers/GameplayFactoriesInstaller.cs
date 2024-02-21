using Code.Runtime.Infrastructure;
using Code.Runtime.Logic.Factories;
using Code.Runtime.Logic.Gameplay;
using Code.Runtime.UI;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Installers
{
    public class GameplayFactoriesInstaller : Installer<ParticleSystem, ParticleSystem, GameplayFactoriesInstaller>
    {
        [Inject] private ParticleSystem _deathVfx;
        [Inject] private ParticleSystem _tapVfx;
        
        public override void InstallBindings()
        {
            BindStatesFactory();

            BindSpawnerFactory();

            BindHUDFactory();

            BindShapeFactory();
            
            BindParticlesFactory();
        }
        
        private void BindStatesFactory()
        {
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
        }

        private void BindSpawnerFactory()
        {
            Container.Bind<Infrastructure.PrefabFactory<Spawner>>().AsSingle();
        }

        private void BindHUDFactory()
        {
            Container.Bind<Infrastructure.PrefabFactory<HUD>>().AsSingle();
        }

        private void BindShapeFactory()
        {
            Container.BindInterfacesTo<ShapeFactory>().AsSingle();
        }
        
        private void BindParticlesFactory()
        {
            Container.BindInterfacesTo<ParticlesFactory>()
                .AsSingle()
                .WithArguments(_tapVfx, _deathVfx);
        }
    }
}