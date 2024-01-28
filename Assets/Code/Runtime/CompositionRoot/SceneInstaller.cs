using Code.Runtime.Logic;
using Zenject;

namespace Code.Runtime.CompositionRoot
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindComboDetector();
        }

        private void BindComboDetector() => 
            Container.BindInterfacesTo<ComboDetector>().AsSingle();
    }
}