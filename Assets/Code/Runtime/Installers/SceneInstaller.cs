using Code.Runtime.Infrastructure;
using Code.Runtime.Logic;
using Zenject;

namespace Code.Runtime.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // BindInput();

            BindComboDetector();
            
            BindStatesFactory();
        }

        private void BindComboDetector() =>
            Container.BindInterfacesTo<ComboDetector>().AsSingle();

        // private void BindInput() => 
        //     Container.BindInterfacesTo<MobileInput>().AsSingle();

        private void BindStatesFactory() =>
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
    }
}