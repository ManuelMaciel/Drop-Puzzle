using Code.Runtime.Logic;
using Zenject;

namespace Code.Runtime.CompositionRoot
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ComboDetector>().AsSingle();
        }
    }
}