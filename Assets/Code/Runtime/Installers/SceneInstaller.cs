﻿using Code.Runtime.Infrastructure;
using Code.Runtime.Logic;
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
            BindInput();

            BindComboDetector();
            
            BindStatesFactory();
            
            BindSpawnerFactory();
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
            Container
                .BindFactory<string, Spawner, Spawner.Factory>()
                .FromFactory<Infrastructure.PrefabFactory<Spawner>>();
        }
    }
}