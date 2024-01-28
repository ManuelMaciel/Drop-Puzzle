using Code.Runtime.Logic;
using Zenject;

namespace Code.Runtime.Installers
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